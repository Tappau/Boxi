using System;
using System.Collections.Generic;

#nullable disable

namespace Boxi.Dal.Models
{
    public partial class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int? OrderId { get; set; }
        public int? ItemId { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }

        public virtual IssueCondition Item { get; set; }
        public virtual Order Order { get; set; }
    }
}
