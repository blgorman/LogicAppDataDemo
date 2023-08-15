using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleProductData;

namespace SimpleProductDataViewer.Controllers
{
    [Authorize]
    public class ResetController : Controller
    {
        private readonly SimpleProductDataDbContext _context;

        public ResetController(SimpleProductDataDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            //clear out the products and new products tables for new run
            _context.Products.RemoveRange(_context.Products);
            _context.NewProducts.RemoveRange(_context.NewProducts);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
