using System;
using System.Collections.Generic;

#nullable disable

namespace Boxi.Dal.Models
{
    public partial class BoxStore
    {
        public BoxStore()
        {
            Issues = new HashSet<Issue>();
        }

        public int BoxId { get; set; }
        public string BoxName { get; set; }
        public string QrData { get; set; }
        public bool? IsActive { get; set; }
        public string Notes { get; set; }

        public virtual ICollection<Issue> Issues { get; set; }
    }
}
