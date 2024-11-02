using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AdminWebpage.Models
{
    public partial class Login
    {
        [Required]
        public string Account { get; set; } = null!;

        [Required]
        [MinLength(5, ErrorMessage = "Password must be at least 5 characters.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Required]
        public string? Role { get; set; } = null!;
    }
}
