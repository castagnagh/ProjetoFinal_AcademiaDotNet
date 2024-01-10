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
    public class ProcedimentosController : Controller
    {
        private readonly Contexto _context;

        public ProcedimentosController(Contexto context)
        {
            _context = context;
        }

        
        public async Task<IActionResult> Index()
        {
              return _context.Procedimentos != null ? 
                          View(await _context.Procedimentos.ToListAsync()) :
                          Problem("Entity set 'Contexto.Procedimentos'  is null.");
        }

        [Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Procedimentos == null)
            {
                return NotFound();
            }

            var procedimento = await _context.Procedimentos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (procedimento == null)
            {
                return NotFound();
            }

            return View(procedimento);
        }

        [Authorize(Roles = "User, Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "User, Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descricao")] Procedimento procedimento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(procedimento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(procedimento);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Procedimentos == null)
            {
                return NotFound();
            }

            var procedimento = await _context.Procedimentos.FindAsync(id);
            if (procedimento == null)
            {
                return NotFound();
            }
            return View(procedimento);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descricao")] Procedimento procedimento)
        {
            if (id != procedimento.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(procedimento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProcedimentoExists(procedimento.Id))
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
            return View(procedimento);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Procedimentos == null)
            {
                return NotFound();
            }

            var procedimento = await _context.Procedimentos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (procedimento == null)
            {
                return NotFound();
            }

            return View(procedimento);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Procedimentos == null)
            {
                return Problem("Entity set 'Contexto.Procedimentos'  is null.");
            }
            var procedimento = await _context.Procedimentos.FindAsync(id);
            if (procedimento != null)
            {
                _context.Procedimentos.Remove(procedimento);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProcedimentoExists(int id)
        {
          return (_context.Procedimentos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
