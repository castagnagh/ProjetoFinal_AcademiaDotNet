using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjetoFinal.Data;
using ProjetoFinal.Models;

namespace ProjetoFinal.Controllers
{
    [Authorize]
    public class MarcasController : Controller
    {
        private readonly Contexto _context;

        public MarcasController(Contexto context)
        {
            _context = context;
        }

        [Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> Index()
        {
              return _context.Marca != null ? 
                          View(await _context.Marca.ToListAsync()) :
                          Problem("Entity set 'Contexto.Marca'  is null.");
        }

        [Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Marca == null)
            {
                return NotFound();
            }

            var marca = await _context.Marca
                .FirstOrDefaultAsync(m => m.Id == id);
            if (marca == null)
            {
                return NotFound();
            }

            return View(marca);
        }

        [Authorize(Roles = "User, Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "User, Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome")] Marca marca)
        {
            if (ModelState.IsValid)
            {
                _context.Add(marca);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(marca);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Marca == null)
            {
                return NotFound();
            }

            var marca = await _context.Marca.FindAsync(id);
            if (marca == null)
            {
                return NotFound();
            }
            return View(marca);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome")] Marca marca)
        {
            if (id != marca.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(marca);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MarcaExists(marca.Id))
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
            return View(marca);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Marca == null)
            {
                return NotFound();
            }

            var marca = await _context.Marca
                .FirstOrDefaultAsync(m => m.Id == id);
            if (marca == null)
            {
                return NotFound();
            }

            return View(marca);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Marca == null)
            {
                return Problem("Entity set 'Contexto.Marca'  is null.");
            }
            var marca = await _context.Marca.FindAsync(id);
            if (marca != null)
            {
                _context.Marca.Remove(marca);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MarcaExists(int id)
        {
          return (_context.Marca?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
