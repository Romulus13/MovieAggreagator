using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class ImdbStoredResult
    {

        public int Id { get; set; }
        public string? ImdbId { get; set; }



        public string? ResultType { get; set; }

        public string? Image { get; set; }

        public string? Title { get; set; }


        public string? Description  { get;set;  }
        


    }
}
