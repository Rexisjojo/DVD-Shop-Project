namespace DVD_Shop.Models
{
    public class Genre
    {
        public int GenreId { get; set; }
        public required string gName { get; set; }
        public required string Slug { get; set; }

        //relations

        public ICollection<Song> Songs { get; set; }
        public ICollection<Movie> Movies { get; set; }
    }
}
