namespace Boxi.Core.Domain
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class Item : BaseEntity
    {
        public Item()
        {
        }

        public Item(string description)
        {
            Description = description;
        }

        public Item(string description, string barcode)
        {
            Description = description;
            Barcode = barcode;
        }

        public Box Box { get; set; }
        public int BoxId { get; set; }
        public string Description { get; set; }
        public string Barcode { get; set; }
    }
}