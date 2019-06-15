using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdministratorPanelMvc.Models
{
    public class Announcement
    {
        public Guid AnnouncementId { get; set; }
        public int Number { get; set; }
        public User User { get; set; }
        public string Text { get; set; }
        public int Rate { get; set; }
        public DateTime CreationDateTime { get; set; }
        public Announcement()
        {
        }
    }
}
