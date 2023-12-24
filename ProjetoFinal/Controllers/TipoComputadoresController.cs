using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjetoFinal.Data;
using ProjetoFinal.Models;

namespace ProjetoFinal.Controllers
{
    public class TipoComputadoresController : Controller
    {
        private readonly Contexto _context;

        public TipoComputadoresController(Contexto context)
        {
            _context = context;
        }

        // GET: TipoComputadores
        public async Task<IActionResult> Index()
        {
              return _context.TipoComputador != null ? 
                          View(await _context.TipoComputador.ToListAsync()) :
                          Problem("Entity set 'Contexto.TipoComputador'  is null.");
        }

        // GET: TipoComputadores/Details/5
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

        // GET: TipoComputadores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoComputadores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: TipoComputadores/Edit/5
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

        // POST: TipoComputadores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: TipoComputadores/Delete/5
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

        // POST: TipoComputadores/Delete/5
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
