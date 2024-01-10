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
    public class TipoComputadoresController : Controller
    {
        private readonly Contexto _context;

        public TipoComputadoresController(Contexto context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
              return _context.TipoComputador != null ? 
                          View(await _context.TipoComputador.ToListAsync()) :
                          Problem("Entity set 'Contexto.TipoComputador'  is null.");
        }

        [Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TipoComputador == null)
            {
                return NotFound();
            }

            var tipoComputador = await _context.TipoComputador
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoComputador == null)
            {
                return NotFound();
            }

            return View(tipoComputador);
        }

        [Authorize(Roles = "User, Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "User, Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome")] TipoComputador tipoComputador)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoComputador);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoComputador);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TipoComputador == null)
            {
                return NotFound();
            }

            var tipoComputador = await _context.TipoComputador.FindAsync(id);
            if (tipoComputador == null)
            {
                return NotFound();
            }
            return View(tipoComputador);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome")] TipoComputador tipoComputador)
        {
            if (id != tipoComputador.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoComputador);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoComputadorExists(tipoComputador.Id))
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
            return View(tipoComputador);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TipoComputador == null)
            {
                return NotFound();
            }

            var tipoComputador = await _context.TipoComputador
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoComputador == null)
            {
                return NotFound();
            }

            return View(tipoComputador);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TipoComputador == null)
            {
                return Problem("Entity set 'Contexto.TipoComputador'  is null.");
            }
            var tipoComputador = await _context.TipoComputador.FindAsync(id);
            if (tipoComputador != null)
            {
                _context.TipoComputador.Remove(tipoComputador);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoComputadorExists(int id)
        {
          return (_context.TipoComputador?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
