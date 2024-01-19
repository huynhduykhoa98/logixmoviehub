using LogixMovieApplication.WebApi.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogixMovieApplication.WebApi.Entities
{
    public class MovieReaction : BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid MovieId { get; set; }

        [ForeignKey("MovieId")]
        public virtual Movie Movie { get; set; }
        public MovieReactionTypeEnum MovieReactionType { get; set; }
    }
}
