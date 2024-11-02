using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AdminWebpage.Models
{
    public partial class TChiTietHdb
    {
        [Required]
        public string SoHdb { get; set; } = null!;

        [Required]
        public string MaThuoc { get; set; } = null!;

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Slban must be a positive number.")]
        public int Slban { get; set; }
        [Required]
        public string KhuyenMai { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "ThanhTien must be a positive number.")]
        public double ThanhTien { get; set; }

        public virtual TThuoc? MaThuocNavigation { get; set; } = null!;

        public virtual THoaDonBan? SoHdbNavigation { get; set; } = null!;
    }
}
