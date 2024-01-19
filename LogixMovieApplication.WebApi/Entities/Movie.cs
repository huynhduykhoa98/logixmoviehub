using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogixMovieApplication.WebApi.Entities
{
    public class Movie : BaseEntity
    {
        public string Title { get; set; }
        public string Thumbnail { get; set; }
    }
}
