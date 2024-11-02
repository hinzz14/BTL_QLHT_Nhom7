using System.Net.Http.Headers;

namespace AdminWebpage.Models
{
    public class Cart
    {
        public List<CartLine> Lines { get; set; } = new List<CartLine>();

        //Hàm thêm sản phẩm
        public void AddItem(TThuoc product, int quantity)
        {
            CartLine? line = Lines.Where(p => p.Product.MaThuoc == product.MaThuoc).FirstOrDefault();
            if (line == null)
            {
                Lines.Add(new CartLine
                {
                    Product = product,
                    Quanity = quantity
                });
            }
            else
            {
                line.Quanity += quantity;
            }
        }

        //Hàm xóa sản phẩm
        public void RemoveLine(TThuoc product) => Lines.RemoveAll(l => l.Product.MaThuoc == product.MaThuoc);
        //Hàm tính tổng tiền
        public decimal ComputeTatalValue() => (decimal)Lines.Sum(e => e.Product?.DonGiaBan * 0.9 * e.Quanity);
        public decimal ComputeTotalQuantity() => (decimal)Lines.Sum(e => e.Quanity);
        //Hàm clear
        public void Clear() => Lines.Clear();


        public class CartLine
        {
            public int CartLineID { get; set; }
            public TThuoc Product { get; set; } = new();
            public int Quanity { get; set; }
            public double ThanhTien => 0.9 * Quanity * Product.DonGiaBan;
        }
    }
}

