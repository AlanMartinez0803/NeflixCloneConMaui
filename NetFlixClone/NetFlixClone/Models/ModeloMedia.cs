using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetFlixClone.Models
{
    public class ModeloMedia
    {
        public int ID { get; set; }
        public string DisplayTitlex { get; set; }
        public string MediaType { get; set; } // "movie" or "tv"
        public string Overview { get; set; }
        public string ReleaseDate { get; set; }
        public string Thumbnail { get; set; }
        public string ThumbnailSmall { get; set; }
        public string ThumbnailUrl { get; set; }
 
    }
}
