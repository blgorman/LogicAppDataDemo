using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleProductData;

namespace SimpleProductDataViewer.Controllers
{
    public class NewProductStagingsController : Controller
    {
        private readonly SimpleProductDataDbContext _context;

        public NewProductStagingsController(SimpleProductDataDbContext context)
        {
            _context = context;
        }

        // GET: NewProductStagings
        public async Task<IActionResult> Index()
        {
              return _context.NewProducts != null ? 
                          View(await _context.NewProducts.ToListAsync()) :
                          Problem("Entity set 'SimpleProductDataDbContext.NewProducts'  is null.");
        }

        // GET: NewProductStagings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.NewProducts == null)
            {
                return NotFound();
            }

            var newProductStaging = await _context.NewProducts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (newProductStaging == null)
            {
                return NotFound();
            }

            return View(newProductStaging);
        }

        private bool NewProductStagingExists(int id)
        {
          return (_context.NewProducts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
