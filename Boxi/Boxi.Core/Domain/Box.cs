using System.Collections.Generic;

namespace Boxi.Core.Domain
{
    public class Box : BaseEntity
    {
        public Box(string boxName, string notes)
        {
            BoxName = boxName;
            Notes = notes;
        }

        public Box(string boxName, string notes, string qrData)
        {
            BoxName = boxName;
            Notes = notes;
            QrData = qrData;
        }

        public string BoxName { get; set; }
        public string QrData { get; set; }
        public string Notes { get; set; }
        public bool IsDeleted { get; set; }

        public ICollection<Item> Items { get; set; }
    }
}