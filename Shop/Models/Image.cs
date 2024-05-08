namespace Shop.Models
{
    public class Image
    {
        public int Id { get; set; }

        public int numberImgs { get; set; }

        public byte[]? img { get; set; }

        public int productId { get; set; }

        public virtual Product Product { get; set; }
    }
}
