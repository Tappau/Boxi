namespace Boxi.Core.Domain
{
    public class Item : BaseEntity
    {
        public string Description { get; set; }

        public string Barcode { get; set; }

        public virtual Box Box { get; set; }
    }
}
