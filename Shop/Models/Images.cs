
namespace Shop.Models
{
    public class Images
    {
        public int Id { get; set; }

        public int numberImgs { get; set; }

        public byte[]? img { get; set; }

        public int productId { get; set; }

        public virtual Product Product { get; set; }

        internal static IDisposable Load(Stream imageStream)
        {
            throw new NotImplementedException();
        }
    }
}
