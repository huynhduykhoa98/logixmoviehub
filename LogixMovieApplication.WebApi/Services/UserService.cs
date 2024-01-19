using BCrypt.Net;
using LogixMovieApplication.WebApi.Data;
using LogixMovieApplication.WebApi.Entities;
using LogixMovieApplication.WebApi.Interfaces;
using LogixMovieApplication.WebApi.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace LogixMovieApplication.Context.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task Register(RegisterVM model)
        {
            var existEmail = await GetByEmail(model.Email);
            if (existEmail != null)
            {
                throw new Exception("Email's already exist");
            }
            var user = new User()
            {
                Email = model.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password),
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.Now,
                LastSavedDate = DateTime.Now,
            };
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Login(string userName, string password)
        {
            var account = await _context.Users.SingleOrDefaultAsync(x => x.Email == userName);

            if (account == null || !BCrypt.Net.BCrypt.Verify(password, account.PasswordHash))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task<User?> GetById(Guid id)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User?> GetByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}
