using LogixMovieApplication.WebApi.Data;
using LogixMovieApplication.WebApi.Entities;
using LogixMovieApplication.WebApi.Enums;
using LogixMovieApplication.WebApi.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LogixMovieApplication.WebApi.Services
{
    public class MovieReactionService : IMovieReactionService
    {
        private readonly ApplicationDbContext _context;
        public MovieReactionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateOrUpdateReaction(Guid userId, Guid movieId, MovieReactionTypeEnum movieReactionType)
        {
            var movieReaction = await _context.MovieReactions.AsNoTracking().FirstOrDefaultAsync(x => x.MovieId == movieId && x.UserId == userId);
            if (movieReaction != null)
            {
                movieReaction.MovieReactionType = movieReactionType;
                movieReaction.LastSavedDate = DateTime.UtcNow;
                _context.MovieReactions.Update(movieReaction);
                await _context.SaveChangesAsync();
            }
            else
            {
                movieReaction = new MovieReaction()
                {
                    MovieId = movieId,
                    UserId = userId,
                    MovieReactionType = movieReactionType,
                    LastSavedDate = DateTime.UtcNow,
                    CreatedDate = DateTime.UtcNow,
                    Id = Guid.NewGuid()
                };
                await _context.MovieReactions.AddAsync(movieReaction);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<MovieReaction>> GetMovieReactions(List<Guid> movieIds)
        {
            return await _context.MovieReactions.AsNoTracking()
                .Where(x => movieIds.Contains(x.MovieId)).ToListAsync();
        }
    }
}
