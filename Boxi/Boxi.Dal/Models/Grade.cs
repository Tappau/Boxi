using System;
using System.Collections.Generic;

#nullable disable

namespace Boxi.Dal.Models
{
    public partial class Grade
    {
        public Grade()
        {
            IssueConditions = new HashSet<IssueCondition>();
        }

        public byte GradeId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<IssueCondition> IssueConditions { get; set; }
    }
}
