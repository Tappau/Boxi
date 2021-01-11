using System;
using System.Collections.Generic;

#nullable disable

namespace Boxi.Dal.Models
{
    public partial class Publisher
    {
        public Publisher()
        {
            Series = new HashSet<Series>();
        }

        public int PublisherId { get; set; }
        public string PubName { get; set; }
        public int? YearBegan { get; set; }
        public string Notes { get; set; }
        public string Url { get; set; }

        public virtual ICollection<Series> Series { get; set; }
    }
}
