namespace DVD_Shop.Models
{
    public class Song
    {
        public int songId { get; set; }
        public required string sTitle { get; set; }

        //uploading song is pending 
        public required string songPath { get; set; }

        public required string Slug { get; set; }

        //relations

        public int ArtistId { get; set; }

        public Artist Artist { get; set; } 
        public int AlbumId { get; set; }
        public Album Album { get; set; }

        public int GenreId { get; set; }
        public Genre Genres { get; set; }

        




    }
}
