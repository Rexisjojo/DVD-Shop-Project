using System.ComponentModel.DataAnnotations;

namespace DVD_Shop.Models
{
    public class Artist
    {
        public int ArtistId { get; set; }
        //[Required]
        public required string aName { get; set; }
        //[Required]
        public required string Description { get; set; }

        //relations
        public int AlbumId { get; set; }    
        public Album Album { get; set; }


        public ICollection<Song> Songs { get; set; }
        public ICollection<Movie> Movies { get; set; }


    }
}
