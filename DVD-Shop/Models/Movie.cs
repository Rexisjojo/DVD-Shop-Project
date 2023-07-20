namespace DVD_Shop.Models
{
    public class Movie
    {
        public int MovieId { get; set; }
        public required string mTitle { get; set; }

        public DateTime ReleaseDate { get; set; }
        public required string mDescription { get; set; }
        public required string CoverImage { get; set; }

        //uploading trailer pending
        public required string TrailerPath { get; set; }

        //relations
        public int AlbumId { get; set; }
        public Album Album { get; set; }

        public int ArtistId { get; set; }
        public Artist Artist { get; set; }


        public int GenreId { get; set; }
        public Genre Genres { get; set; }

    }
}
