﻿using System.ComponentModel.DataAnnotations;

namespace FileManagementStudio.Server.DTOs
{
    public class RegisterDto
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
