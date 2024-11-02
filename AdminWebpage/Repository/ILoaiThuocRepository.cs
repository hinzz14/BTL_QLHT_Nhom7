using AdminWebpage.Models;

namespace AdminWebpage.Repository
{
    public interface ILoaiThuocRepository
    {
        TLoaiThuoc Add(TLoaiThuoc loaiThuoc);
        TLoaiThuoc Update(TLoaiThuoc loaithuoc);

        TLoaiThuoc Delete(string maLoaiThuoc);
        TLoaiThuoc GetLoaiThuoc(string maLoaiThuoc);

        IEnumerable<TLoaiThuoc> GetAll();
    }
}
