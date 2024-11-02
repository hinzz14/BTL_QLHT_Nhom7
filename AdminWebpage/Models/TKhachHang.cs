using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AdminWebpage.Models
{
    public partial class TKhachHang
    {
        [Required]
        public string MaKh { get; set; } = null!;

        [Required]
        public string TenKh { get; set; } = null!;

        public string GioiTinh { get; set; }

        [DataType(DataType.Date)]
        public DateTime NgaySinh { get; set; }
        [Required]
        public string DiaChi { get; set; }
        [Required]
        [RegularExpression(@"^\d{10,11}$", ErrorMessage = "Invalid phone number.")]
        public string? Sdt { get; set; }

        public virtual ICollection<THoaDonBan> THoaDonBans { get; set; } = new List<THoaDonBan>();
    }
}
