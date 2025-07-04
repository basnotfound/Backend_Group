using CoopManagementApp.Data;
using CoopManagementApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CoopManagementApp.Controllers
{
    public class TransactionController : Controller
    {
        private readonly CoopManagementContext _context;

        public TransactionController(CoopManagementContext context)
        {
            _context = context;
        }

        // GET: Transaction/Index
        public async Task<IActionResult> Index()
        {
            var transactions = await _context.Transactions
                .Include(t => t.Product)
                .Include(t => t.Customer)
                .ToListAsync();
            return View(transactions);
        }

        // GET: Transaction/Create
        public IActionResult Create()
        {
            // Get list of customers and products for the dropdowns
            ViewData["CustomerList"] = new SelectList(_context.Customer, "id_cus", "name_cus");
            ViewData["ProductList"] = new SelectList(_context.Product, "id_product", "name_product");
            return View();
        }

        // POST: Transaction/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_cus, id_product, quantity, total_price, date_transaction")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                // Fetch the product to calculate the total price
                var product = await _context.Product.FindAsync(transaction.id_product);

                if (product != null)
                {
                    transaction.total_price = product.cost * transaction.quantity;
                    transaction.date_transaction = DateTime.Now; // Set the current time for the transaction
                    _context.Add(transaction);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "Invalid Product selected");
            }

            ViewData["CustomerList"] = new SelectList(_context.Customer, "id_cus", "name_cus", transaction.id_cus);
            ViewData["ProductList"] = new SelectList(_context.Product, "id_product", "name_product", transaction.id_product);
            return View(transaction);
        }

        // GET: Transaction/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions
                .Include(t => t.Product)
                .Include(t => t.Customer)
                .FirstOrDefaultAsync(t => t.id_transaction == id);

            if (transaction == null)
            {
                return NotFound();
            }

            ViewData["CustomerList"] = new SelectList(_context.Customer, "id_cus", "name_cus", transaction.id_cus);
            ViewData["ProductList"] = new SelectList(_context.Product, "id_product", "name_product", transaction.id_product);
            return View(transaction);
        }

        // POST: Transaction/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id_transaction, id_cus, id_product, quantity, total_price, date_transaction")] Transaction transaction)
        {
            if (id != transaction.id_transaction)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Fetch the product to recalculate the total price
                    var product = await _context.Product.FindAsync(transaction.id_product);
                    if (product != null)
                    {
                        transaction.total_price = product.cost * transaction.quantity;
                        _context.Update(transaction);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid Product selected");
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransactionExists(transaction.id_transaction))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerList"] = new SelectList(_context.Customer, "id_cus", "name_cus", transaction.id_cus);
            ViewData["ProductList"] = new SelectList(_context.Product, "id_product", "name_product", transaction.id_product);
            return View(transaction);
        }

        // GET: Transaction/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions
                .Include(t => t.Product)
                .Include(t => t.Customer)
                .FirstOrDefaultAsync(t => t.id_transaction == id);

            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // POST: Transaction/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction != null)
            {
                _context.Transactions.Remove(transaction);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool TransactionExists(int id)
        {
            return _context.Transactions.Any(e => e.id_transaction == id);
        }
    }
}