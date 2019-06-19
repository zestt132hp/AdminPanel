using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class AnnouncementDto
    {
        [Required]
        [Range(1, Int64.MaxValue, ErrorMessage = "Недопустимый номер объявления")]
        public int Number { get; set; }
        [StringLength(50, MinimumLength = 3)]
        public string UserName { get; set; }
        [StringLength(250, MinimumLength = 10)]
        public string Text { get; set; }
    }
}
