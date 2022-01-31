using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class YoutubeStoredResult
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public YoutubeResultTypeEnum Type { get; set; }

        public string? ResourceId { get; set; }

        public string? Description { get; set; }
    }
}
