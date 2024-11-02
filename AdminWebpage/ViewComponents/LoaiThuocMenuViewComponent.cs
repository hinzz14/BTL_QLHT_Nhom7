using AdminWebpage.Models;
using AdminWebpage.Repository;
using Microsoft.AspNetCore.Mvc;

namespace AdminWebpage.ViewComponents
{
    public class LoaiThuocMenuViewComponent : ViewComponent
    {
        private readonly ILoaiThuocRepository _loaiThuoc;

        public LoaiThuocMenuViewComponent(ILoaiThuocRepository loaiThuocRepository)
        {
            _loaiThuoc = loaiThuocRepository;
        }

        public IViewComponentResult Invoke()
        {
            var loaiThuoc = _loaiThuoc.GetAll().OrderBy(x => x.MaLoai);

            return View(loaiThuoc);
        }
    }
}
