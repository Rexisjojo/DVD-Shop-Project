namespace DVD_Shop.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public required string CName { get; set; }
        public required string Slug { get; set; }
        public bool Cstatus { get; set; }

        //relations
        public ICollection<Album> Albums { get; set; }
    }
}
