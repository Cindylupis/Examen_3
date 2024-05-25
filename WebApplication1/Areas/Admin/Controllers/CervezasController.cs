using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CervezasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CervezasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Cervezas
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Cervezas.Include(c => c.Estilo);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/Cervezas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Cervezas == null)
            {
                return NotFound();
            }

            var cerveza = await _context.Cervezas
                .Include(c => c.Estilo)
                .FirstOrDefaultAsync(m => m.id == id);
            if (cerveza == null)
            {
                return NotFound();
            }

            return View(cerveza);
        }

        // GET: Admin/Cervezas/Create
        public IActionResult Create()
        {
            ViewData["idEstilo"] = new SelectList(_context.Estilos, "id", "Nombre");
            return View();
        }

        // POST: Admin/Cervezas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,nombre,alcohol,idEstilo,precio")] Cerveza cerveza)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cerveza);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["idEstilo"] = new SelectList(_context.Estilos, "id", "Nombre", cerveza.idEstilo);
            return View(cerveza);
        }

        // GET: Admin/Cervezas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Cervezas == null)
            {
                return NotFound();
            }

            var cerveza = await _context.Cervezas.FindAsync(id);
            if (cerveza == null)
            {
                return NotFound();
            }
            ViewData["idEstilo"] = new SelectList(_context.Estilos, "id", "Nombre", cerveza.idEstilo);
            return View(cerveza);
        }

        // POST: Admin/Cervezas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,nombre,alcohol,idEstilo,precio")] Cerveza cerveza)
        {
            if (id != cerveza.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cerveza);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CervezaExists(cerveza.id))
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
            ViewData["idEstilo"] = new SelectList(_context.Estilos, "id", "Nombre", cerveza.idEstilo);
            return View(cerveza);
        }

        // GET: Admin/Cervezas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Cervezas == null)
            {
                return NotFound();
            }

            var cerveza = await _context.Cervezas
                .Include(c => c.Estilo)
                .FirstOrDefaultAsync(m => m.id == id);
            if (cerveza == null)
            {
                return NotFound();
            }

            return View(cerveza);
        }

        // POST: Admin/Cervezas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Cervezas == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Cervezas'  is null.");
            }
            var cerveza = await _context.Cervezas.FindAsync(id);
            if (cerveza != null)
            {
                _context.Cervezas.Remove(cerveza);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CervezaExists(int id)
        {
          return (_context.Cervezas?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
