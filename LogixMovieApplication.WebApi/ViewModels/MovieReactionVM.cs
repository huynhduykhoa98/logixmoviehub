using LogixMovieApplication.WebApi.Enums;

namespace LogixMovieApplication.WebApi.ViewModels
{
    public class MovieReactionVM
    {
        public Guid MovieId { get; set; }

        public MovieReactionTypeEnum MovieReactionType { get; set; }
    }
}
