using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FirstWebApplication.Models;

namespace FirstWebApplication.Controllers
{
    public class OrderProductConnectionsController : Controller
    {
        private readonly ShopDbContext _context;

        public OrderProductConnectionsController(ShopDbContext context)
        {
            _context = context;
        }

        // GET: OrderProductConnections
        public async Task<IActionResult> Index()
        {
            var shopDbContext = _context.orderProductConnections.Include(o => o.Order).Include(o => o.Product);
            return View(await shopDbContext.ToListAsync());
        }

        // GET: OrderProductConnections/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderProductConnection = await _context.orderProductConnections
                .Include(o => o.Order)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (orderProductConnection == null)
            {
                return NotFound();
            }

            return View(orderProductConnection);
        }

        // GET: OrderProductConnections/Create
        public IActionResult Create()
        {
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "DatePropertyText");
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Name");
            return View();
        }

        // POST: OrderProductConnections/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,ProductId,Quantity")] OrderProductConnection orderProductConnection)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderProductConnection);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "DatePropertyText", orderProductConnection.OrderId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Name", orderProductConnection.ProductId);
            return View(orderProductConnection);
        }

        // GET: OrderProductConnections/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderProductConnection = await _context.orderProductConnections.FindAsync(id);
            if (orderProductConnection == null)
            {
                return NotFound();
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "DatePropertyText", orderProductConnection.OrderId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Name", orderProductConnection.ProductId);
            return View(orderProductConnection);
        }

        // POST: OrderProductConnections/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,ProductId,Quantity")] OrderProductConnection orderProductConnection)
        {
            if (id != orderProductConnection.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderProductConnection);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderProductConnectionExists(orderProductConnection.OrderId))
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
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "DatePropertyText", orderProductConnection.OrderId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Name", orderProductConnection.ProductId);
            return View(orderProductConnection);
        }

        // GET: OrderProductConnections/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderProductConnection = await _context.orderProductConnections
                .Include(o => o.Order)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (orderProductConnection == null)
            {
                return NotFound();
            }

            return View(orderProductConnection);
        }

        // POST: OrderProductConnections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orderProductConnection = await _context.orderProductConnections.FindAsync(id);
            if (orderProductConnection != null)
            {
                _context.orderProductConnections.Remove(orderProductConnection);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderProductConnectionExists(int id)
        {
            return _context.orderProductConnections.Any(e => e.OrderId == id);
        }
    }
}
