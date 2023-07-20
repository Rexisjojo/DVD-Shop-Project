using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DVD_Shop.Models
{
    public class Playlist
    {
        public int PlaylistId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public ICollection<Song> Songs { get; set; }
    }
}