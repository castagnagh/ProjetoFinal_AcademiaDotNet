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
    public class ComputadoresController : Controller
    {
        private readonly Contexto _context;

        public ComputadoresController(Contexto context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var contexto = _context.Computadores.Include(c => c.Marca).Include(c => c.Secao).Include(c => c.TipoComputador);
            return View(await contexto.ToListAsync());
        }

        [Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Computadores == null)
            {
                return NotFound();
            }

            var computador = await _context.Computadores
                .Include(c => c.Marca)
                .Include(c => c.Secao)
                .Include(c => c.TipoComputador)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (computador == null)
            {
                return NotFound();
            }

            return View(computador);
        }
        [Authorize(Roles = "User, Admin")]
        public IActionResult Create()
        {
            ViewData["MarcaId"] = new SelectList(_context.Set<Marca>(), "Id", "Nome");
            ViewData["SecaoId"] = new SelectList(_context.Secoes, "Id", "Nome");
            ViewData["TipoComputadorId"] = new SelectList(_context.Set<TipoComputador>(), "Id", "Nome");
            return View();
        }

        [Authorize(Roles = "User, Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NumPatrimonio,Descricao,SecaoId,MarcaId,TipoComputadorId")] Computador computador)
        {
            if (ModelState.IsValid)
            {
                _context.Add(computador);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MarcaId"] = new SelectList(_context.Set<Marca>(), "Id", "Nome", computador.MarcaId);
            ViewData["SecaoId"] = new SelectList(_context.Secoes, "Id", "Nome", computador.SecaoId);
            ViewData["TipoComputadorId"] = new SelectList(_context.Set<TipoComputador>(), "Id", "Nome", computador.TipoComputadorId);
            return View(computador);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Computadores == null)
            {
                return NotFound();
            }

            var computador = await _context.Computadores.FindAsync(id);
            if (computador == null)
            {
                return NotFound();
            }
            ViewData["MarcaId"] = new SelectList(_context.Set<Marca>(), "Id", "Nome", computador.MarcaId);
            ViewData["SecaoId"] = new SelectList(_context.Secoes, "Id", "Nome", computador.SecaoId);
            ViewData["TipoComputadorId"] = new SelectList(_context.Set<TipoComputador>(), "Id", "Nome", computador.TipoComputadorId);
            return View(computador);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NumPatrimonio,Descricao,SecaoId,MarcaId,TipoComputadorId")] Computador computador)
        {
            if (id != computador.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(computador);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComputadorExists(computador.Id))
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
            ViewData["MarcaId"] = new SelectList(_context.Set<Marca>(), "Id", "Nome", computador.MarcaId);
            ViewData["SecaoId"] = new SelectList(_context.Secoes, "Id", "Nome", computador.SecaoId);
            ViewData["TipoComputadorId"] = new SelectList(_context.Set<TipoComputador>(), "Id", "Nome", computador.TipoComputadorId);
            return View(computador);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Computadores == null)
            {
                return NotFound();
            }

            var computador = await _context.Computadores
                .Include(c => c.Marca)
                .Include(c => c.Secao)
                .Include(c => c.TipoComputador)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (computador == null)
            {
                return NotFound();
            }

            return View(computador);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Computadores == null)
            {
                return Problem("Entity set 'Contexto.Computadores'  is null.");
            }
            var computador = await _context.Computadores.FindAsync(id);
            if (computador != null)
            {
                _context.Computadores.Remove(computador);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComputadorExists(int id)
        {
          return (_context.Computadores?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
