using LogixMovieApplication.WebApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogixMovieApplication.WebApi.ViewModels
{
    public class MovieVM
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Thumbnail { get; set; }
        public int LikeCount { get; set; }
        public int DislikeCount { get; set; }
        public bool Liked { get; set; }
        public bool Disliked { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
