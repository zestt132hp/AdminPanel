using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class UserDto
    {

        [StringLength(50, MinimumLength = 3)]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mail { get; set; }
        public string Phone { get; set; }
    }
}
