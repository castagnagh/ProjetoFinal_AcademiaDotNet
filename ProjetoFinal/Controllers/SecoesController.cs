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
    public class SecoesController : Controller
    {
        private readonly Contexto _context;

        public SecoesController(Contexto context)
        {
            _context = context;
        }
        [Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> Index()
        {
              return _context.Secoes != null ? 
                          View(await _context.Secoes.ToListAsync()) :
                          Problem("Entity set 'Contexto.Secoes'  is null.");
        }
        [Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Secoes == null)
            {
                return NotFound();
            }

            var secao = await _context.Secoes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (secao == null)
            {
                return NotFound();
            }

            return View(secao);
        }

        [Authorize(Roles = "User, Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "User, Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome")] Secao secao)
        {
            if (ModelState.IsValid)
            {
                _context.Add(secao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(secao);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Secoes == null)
            {
                return NotFound();
            }

            var secao = await _context.Secoes.FindAsync(id);
            if (secao == null)
            {
                return NotFound();
            }
            return View(secao);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome")] Secao secao)
        {
            if (id != secao.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(secao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SecaoExists(secao.Id))
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
            return View(secao);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Secoes == null)
            {
                return NotFound();
            }

            var secao = await _context.Secoes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (secao == null)
            {
                return NotFound();
            }

            return View(secao);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Secoes == null)
            {
                return Problem("Entity set 'Contexto.Secoes'  is null.");
            }
            var secao = await _context.Secoes.FindAsync(id);
            if (secao != null)
            {
                _context.Secoes.Remove(secao);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SecaoExists(int id)
        {
          return (_context.Secoes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
