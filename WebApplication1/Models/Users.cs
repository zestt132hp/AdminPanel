﻿using System;

namespace AdministratorPanelMvc.Models
{
    public class User
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }

        public User()
        {
        }
    }
}
