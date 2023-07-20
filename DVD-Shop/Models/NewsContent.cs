using System.ComponentModel.DataAnnotations;

namespace DVD_Shop.Models
{
    public class NewsContent
    {
        public int NewsContentId { get; set; }

        [Display(Name = "News Title")]
        public required string NewsTitle { get; set; }
        [StringLength(250)]
        [Display(Name = "News")]
        public required string News { get; set; }
    }
}
