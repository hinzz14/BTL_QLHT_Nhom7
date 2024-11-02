using AdminWebpage.Models;

namespace AdminWebpage.Repository
{
    public class LoaiThuocRepository : ILoaiThuocRepository
    {
        private readonly QuanLyHieuThuocWebContext _context;

        public LoaiThuocRepository(QuanLyHieuThuocWebContext context)
        {
            _context = context;
        }

        public TLoaiThuoc Add(TLoaiThuoc loaiThuoc)
        {
            _context.TLoaiThuocs.Add(loaiThuoc);
            _context.SaveChanges();
            return loaiThuoc;
        }

        public TLoaiThuoc Delete(string maLoaiThuoc)
        {
            throw new NotImplementedException();
        }

        public TLoaiThuoc Update(TLoaiThuoc loaiThuoc)
        {
            _context.Update(loaiThuoc);
            _context.SaveChanges();
            return loaiThuoc;
        }

        public IEnumerable<TLoaiThuoc> GetAll()
        {
            return _context.TLoaiThuocs;
        }

        public TLoaiThuoc GetLoaiThuoc(string maLoai)
        {
            return _context.TLoaiThuocs.Find(maLoai);
        }
    }
}
