namespace Shop.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string name { get; set; }

        public string description { get; set; }

         public virtual IEnumerable<Product> products { get; set; }

        public virtual IEnumerable<CellphoneProperties> cellphoneProperties { get; set; }

        public virtual IEnumerable<TVProperties> tvProperties { get; set; }

        public virtual IEnumerable<PhotoProperties> photoProperties { get; set; }

       
    }
}
