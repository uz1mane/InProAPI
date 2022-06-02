﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Lab_1.Models
{
    [Index(nameof(Username), IsUnique = true)]
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }   
        [Required]
        public bool IsPremium { get; set; }

        public string? Token { get; set; }

    }
}
