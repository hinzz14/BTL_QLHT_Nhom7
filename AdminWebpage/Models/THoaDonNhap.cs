using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AdminWebpage.Models
{
    public partial class THoaDonNhap
    {
        [Required]
        public string SoHdn { get; set; }

        [Required]
        public string MaNv { get; set; }

        [DataType(DataType.Date)]
        public DateTime NgayLap { get; set; }

        [Required]
        public string MaNcc { get; set; }

        [Required]
        public double TongTien { get; set; }

        public virtual TNhaCungCap? MaNccNavigation { get; set; }

        public virtual TNhanVien? MaNvNavigation { get; set; }

        public virtual ICollection<TChiTietHdn> TChiTietHdns { get; set; } = new List<TChiTietHdn>();
    }
}
