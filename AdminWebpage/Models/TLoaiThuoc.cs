using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AdminWebpage.Models
{
    public partial class TLoaiThuoc
    {
        [Required]
        public string MaLoai { get; set; } = null!;

        [Required]
        public string TenLoai { get; set; } = null!;

        public virtual ICollection<TThuoc> TThuocs { get; set; } = new List<TThuoc>();
    }
}
