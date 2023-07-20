using System.ComponentModel.DataAnnotations;

namespace DVD_Shop.Models
{
    public class Album
    {
        public int AlbumId { get; set; }
        public required string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public required string Description { get; set; }
       
        [Required]
        [Display(Name = "Album Price")]
        public int APrice { get; set; }
        public required string ImageURL { get; set; }

        //relations
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public ICollection<Artist> Artists { get; set; }
        public ICollection<Song> Songs { get; set; }
        public ICollection<Movie> Movies { get; set; }


    }
}
