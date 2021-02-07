using System.Collections.Generic;

namespace Boxi.Core.Domain
{
    public class Box : BaseEntity
    {
        public string BoxName { get; set; }
        public string QrData { get; set; }
        public string Notes { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<Item> Items { get; set; }
    }
}
