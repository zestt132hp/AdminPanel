using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Announcement
    {
        [RegularExpression(@"(?im)^[{(]?[0-9A-F]{8}[-]?(?:[0-9A-F]{4}[-]?){3}[0-9A-F]{12}[)}]?$")]
        public Guid AnnouncementId { get; set; }
        [Required]
        [Range(1, Int64.MaxValue, ErrorMessage = "Недопустимый номер объявления")]
        public int Number { get; set; }
        public User User { get; set; }
        public string Text { get; set; }
        [Required]
        [Range(1, 10, ErrorMessage = "Недопустимый рейтинг")]
        public int Rate { get; set; }
        public DateTime CreationDateTime { get; set; }
    }
}
