using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AdminWebpage.Models
{
    public partial class TNhaCungCap
    {
        [Required]
        public string MaNcc { get; set; } = null!;

        [Required]
        public string TenNcc { get; set; } = null!;
        [Required]
        [RegularExpression(@"^\d{10,11}$", ErrorMessage = "Invalid phone number.")]
        public string Sdt { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [RegularExpression("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$", ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; }
        [Required]
        public string DiaChi { get; set; }

        public virtual ICollection<THoaDonNhap> THoaDonNhaps { get; set; } = new List<THoaDonNhap>();
    }
}
