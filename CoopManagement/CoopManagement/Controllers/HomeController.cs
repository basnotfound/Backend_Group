using CoopManagementApp.Data;
using CoopManagementApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoopManagementApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly CoopManagementContext _context;

        public HomeController(CoopManagementContext context)
        {
            _context = context;
        }

        // GET: Home/Index
        public async Task<IActionResult> Index()
        {
            var model = new HomeViewModel
            {
                Customers = await _context.Customer.ToListAsync(),
                Products = await _context.Product.ToListAsync(),
                Transactions = await _context.Transactions
                                              .Include(t => t.Product)
                                              .Include(t => t.Customer)
                                              .ToListAsync()
            };

            return View(model);
        }
    }
}