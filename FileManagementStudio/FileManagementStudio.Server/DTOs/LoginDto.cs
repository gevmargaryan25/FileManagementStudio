﻿using System.ComponentModel.DataAnnotations;

namespace FileManagementStudio.Server.DTOs
{
    public class LoginDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
