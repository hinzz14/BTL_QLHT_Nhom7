using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AdminWebpage.Models
{
    public partial class TNhanVien
    {
        [Required]
        public string MaNv { get; set; } = null!;

        [Required]
        public string TenNv { get; set; } = null!;
        [Required]
        public string GioiTinh { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime NgaySinh { get; set; }
        [Required]
        public string DiaChi { get; set; }

        [RegularExpression(@"^\d{10,11}$", ErrorMessage = "Invalid phone number.")]
        [Required]
        public string Sdt { get; set; }

        public virtual ICollection<THoaDonBan> THoaDonBans { get; set; } = new List<THoaDonBan>();

        public virtual ICollection<THoaDonNhap> THoaDonNhaps { get; set; } = new List<THoaDonNhap>();
    }
}
