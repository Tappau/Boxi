using System;
using System.Collections.Generic;

#nullable disable

namespace Boxi.Dal.Models
{
    public partial class IssueCondition
    {
        public IssueCondition()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int IssueConditionId { get; set; }
        public int? IssueId { get; set; }
        public byte? GradeId { get; set; }
        public int? Quantity { get; set; }

        public virtual Grade Grade { get; set; }
        public virtual Issue Issue { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
