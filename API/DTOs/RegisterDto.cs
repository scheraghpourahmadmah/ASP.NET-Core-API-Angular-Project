﻿using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "This website requires username")] 
        public string Username { get; set; }
        [Required]
        [StringLength(8,MinimumLength = 4)]
        public string Password { get; set; }
    }
}
