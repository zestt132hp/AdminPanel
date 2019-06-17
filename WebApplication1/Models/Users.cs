using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class User
    {
        [RegularExpression(@"(?im)^[{(]?[0-9A-F]{8}[-]?(?:[0-9A-F]{4}[-]?){3}[0-9A-F]{12}[)}]?$")]
        public Guid UserId { get; set; }
        [Required(ErrorMessage = "Не указано имя")]
        public string Name { get; set; }
    }
}
