using LogixMovieApplication.WebApi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace LogixMovieApplication.WebApi.Data
{
    public class DbInitializer
    {
        public ApplicationDbContext _context;
        public DbInitializer(ApplicationDbContext context)
        {
            this._context = context;
        }
        public async Task RunAsync()
        {
            await _context.Database.MigrateAsync();
            await _context.Database.EnsureCreatedAsync();
            await _context.SaveChangesAsync();
            await SeedingMovies();

        }
        async Task SeedingMovies()
        {
            if (!await _context.Movies.AnyAsync())
            {
                List<Movie> movies = new List<Movie>()
                {
                    new Movie()
                    {
                        Title = "Iron Man 4",
                        Thumbnail = "https://i.ytimg.com/vi/pWqUEc7odAo/maxresdefault.jpg",
                        Id = Guid.NewGuid(),
                        CreatedDate = DateTime.Now,
                        LastSavedDate = DateTime.Now,
                    },
                    new Movie()
                    {
                        Title = "Spider man",
                        Thumbnail = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQQcZlscZ5KL9y2T4pd0Re_utIQ5m3xslqYxBsKW6u3lA&s",
                        Id = Guid.NewGuid(),
                        CreatedDate = DateTime.Now,
                        LastSavedDate = DateTime.Now,
                    },
                    new Movie()
                    {
                        Title = "Wonder Woman",
                        Thumbnail = "https://kenh14cdn.com/thumb_w/660/2017/1-1499966269752.jpg",
                        Id = Guid.NewGuid(),
                        CreatedDate = DateTime.Now,
                        LastSavedDate = DateTime.Now,
                    },
                    new Movie()
                    {
                        Title = "Bat Man",
                        Thumbnail = "https://images2.thanhnien.vn/528068263637045248/2023/8/16/batman-16921754182593105683.jpg",
                        Id = Guid.NewGuid(),
                        CreatedDate = DateTime.Now,
                        LastSavedDate = DateTime.Now,
                    }
                };
                await _context.Movies.AddRangeAsync(movies);
                await _context.SaveChangesAsync();
            }
        }
    }
}
