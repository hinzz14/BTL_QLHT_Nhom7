using AdminWebpage.Infrastructure;
using AdminWebpage.Models;
using Microsoft.AspNetCore.Mvc;

namespace AdminWebpage.ViewComponents
{
    public class CartWidget:ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View(HttpContext.Session.GetJson<Cart>("cart"));
        }
    }
}
