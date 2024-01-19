using LogixMovieApplication.WebApi.Entities;

namespace LogixMovieApplication.WebApi.Interfaces
{
    public interface IMovieReactionService
    {
        Task CreateOrUpdateReaction(Guid userId, Guid movieId, Enums.MovieReactionTypeEnum movieReactionType);
        Task<List<MovieReaction>> GetMovieReactions(List<Guid> movieIds);
    }
}
