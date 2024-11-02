using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AdminWebpage.Models
{
    public partial class TThuoc
    {
        [Required]
        public string MaThuoc { get; set; } = null!;

        [Required]
        public string TenThuoc { get; set; } = null!;

        public string? ThanhPhan { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime NgaySx { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime NgayHh { get; set; }

        public string? MaLoai { get; set; }

        [Required]
        public double DonGiaBan { get; set; }
        [Required]
        public double DonGiaNhap { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "SoLuong must be a non-negative number.")]
        public int SoLuong { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "TrongLuong must be a non-negative number.")]
        public double? TrongLuong { get; set; }

        public string? Anh { get; set; }

        [JsonIgnore]
        public virtual TLoaiThuoc? MaLoaiNavigation { get; set; }

        public virtual ICollection<TChiTietHdb> TChiTietHdbs { get; set; } = new List<TChiTietHdb>();

        public virtual ICollection<TChiTietHdn> TChiTietHdns { get; set; } = new List<TChiTietHdn>();
    }
}
