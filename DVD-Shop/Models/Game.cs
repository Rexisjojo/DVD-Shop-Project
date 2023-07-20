namespace DVD_Shop.Models
{
    public class Game
    {
        public int gameId { get; set; }
        public required string gTitle { get; set; }
        public DateTime ReleaseDate { get; set; }
        public required string gDespriction { get; set; }
        
        
        //uploading trailer pending

        public required string CoverImage { get; set; }

        public required string TrailerPath { get; set; }

    }
}
