using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AdminWebpage.Models
{
    public partial class THoaDonBan
    {
        [Required]
        public string SoHdb { get; set; }

        [Required]
        public string MaNv { get; set; }

        [DataType(DataType.Date)]
        public DateTime NgayLap { get; set; }

        [Required]
        public string MaKh { get; set; }

        [Required]
        public double TongTien { get; set; }

        public virtual TKhachHang? MaKhNavigation { get; set; }

        public virtual TNhanVien? MaNvNavigation { get; set; }

        public virtual ICollection<TChiTietHdb> TChiTietHdbs { get; set; } = new List<TChiTietHdb>();
    }
}
