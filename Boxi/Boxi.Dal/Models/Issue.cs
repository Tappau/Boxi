using System;
using System.Collections.Generic;

#nullable disable

namespace Boxi.Dal.Models
{
    public partial class Issue
    {
        public Issue()
        {
            IssueConditions = new HashSet<IssueCondition>();
        }

        public int IssueId { get; set; }
        public int SeriesId { get; set; }
        public int? BoxId { get; set; }
        public string Number { get; set; }
        public string PublicationDate { get; set; }
        public decimal? PageCount { get; set; }
        public string Frequency { get; set; }
        public string Editor { get; set; }
        public string Isbn { get; set; }
        public string Barcode { get; set; }
        public int? Writer { get; set; }
        public int? Pencils { get; set; }
        public int? Inking { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? AddedOn { get; set; }
        public int? GcdissueNumber { get; set; }

        public virtual BoxStore Box { get; set; }
        public virtual Series Series { get; set; }
        public virtual ICollection<IssueCondition> IssueConditions { get; set; }
    }
}
