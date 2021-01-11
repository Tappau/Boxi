using System;
using System.Collections.Generic;

#nullable disable

namespace Boxi.Dal.Models
{
    public partial class Series
    {
        public Series()
        {
            Issues = new HashSet<Issue>();
        }

        public int SeriesId { get; set; }
        public string SeriesName { get; set; }
        public int? YearBegan { get; set; }
        public int? YearEnd { get; set; }
        public string Dimensions { get; set; }
        public string PaperStock { get; set; }
        public int PublisherId { get; set; }

        public virtual Publisher Publisher { get; set; }
        public virtual ICollection<Issue> Issues { get; set; }
    }
}
