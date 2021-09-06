using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YMG.Models
{
    public class Award
    {
        public int AwardId { get; set; }
        public string Name { get; set; }
        public string Year { get; set; }
        public string Category { get; set; }
        public virtual List<Actor> Winners { get; set; }
    }
}