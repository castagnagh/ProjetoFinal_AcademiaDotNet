using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjetoFinal.Data;
using ProjetoFinal.Models;

namespace ProjetoFinal.Controllers
{
    public class RegistroManutencoesController : Controller
    {
        private readonly Contexto _context;

        public RegistroManutencoesController(Contexto context)
        {
            _context = context;
        }

        // GET: RegistroManutencoes
        public async Task<IActionResult> Index()
        {
            var contexto = _context.RegistroManutencoes.Include(r => r.Computador).Include(r => r.Procedimento);
            return View(await contexto.ToListAsync());
        }

        // GET: RegistroManutencoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.RegistroManutencoes == null)
            {
                return NotFound();
            }

            var registroManutencao = await _context.RegistroManutencoes
                .Include(r => r.Computador)
                .Include(r => r.Procedimento)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (registroManutencao == null)
            {
                return NotFound();
            }

            return View(registroManutencao);
        }

        // GET: RegistroManutencoes/Create
        public IActionResult Create()
        {
            var viewModel = new RegistroManutViewModel
            {
                ComputadorList = _context.Computadores
                    .Include(c => c.Marca)
                    .Include(c => c.TipoComputador)
                    .Include(c => c.Secao)
                    .ToList(),
                Marcas = _context.Marca.ToList(),
                TiposComputador = _context.TipoComputador.ToList(),
                Secoes = _context.Secoes.ToList(),
                RegistroManutencao = new RegistroManutencao()
            };

            ViewData["ProcedimentoId"] = new SelectList(_context.Procedimentos, "Id", "Descricao");

            return View(viewModel);
        }

        // POST: RegistroManutencoes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DataManutencao,DataPrevisao,ComputadorId,ProcedimentoId")] RegistroManutencao registroManutencao)
        {
            if (ModelState.IsValid)
            {
                _context.Add(registroManutencao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var viewModel = new RegistroManutViewModel
            {
                RegistroManutencao = registroManutencao,
                ComputadorList = _context.Computadores.ToList(),
            };
            ViewData["ComputadorId"] = new SelectList(_context.Computadores, "Id", "Descricao", registroManutencao.ComputadorId);
            ViewData["ProcedimentoId"] = new SelectList(_context.Procedimentos, "Id", "Descricao", registroManutencao.ProcedimentoId);

            return View(viewModel);
        }


        // GET: RegistroManutencoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.RegistroManutencoes == null)
            {
                return NotFound();
            }

            var registroManutencao = await _context.RegistroManutencoes.FindAsync(id);
            if (registroManutencao == null)
            {
                return NotFound();
            }
            ViewData["ComputadorId"] = new SelectList(_context.Computadores, "Id", "Descricao", registroManutencao.ComputadorId);
            ViewData["ProcedimentoId"] = new SelectList(_context.Procedimentos, "Id", "Descricao", registroManutencao.ProcedimentoId);
            return View(registroManutencao);
        }

        // POST: RegistroManutencoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DataManutencao,DataPrevisao,ComputadorId,ProcedimentoId")] RegistroManutencao registroManutencao)
        {
            if (id != registroManutencao.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(registroManutencao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegistroManutencaoExists(registroManutencao.Id))
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
            ViewData["ComputadorId"] = new SelectList(_context.Computadores, "Id", "Descricao", registroManutencao.ComputadorId);
            ViewData["ProcedimentoId"] = new SelectList(_context.Procedimentos, "Id", "Descricao", registroManutencao.ProcedimentoId);
            return View(registroManutencao);
        }

        // GET: RegistroManutencoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.RegistroManutencoes == null)
            {
                return NotFound();
            }

            var registroManutencao = await _context.RegistroManutencoes
                .Include(r => r.Computador)
                .Include(r => r.Procedimento)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (registroManutencao == null)
            {
                return NotFound();
            }

            return View(registroManutencao);
        }

        // POST: RegistroManutencoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.RegistroManutencoes == null)
            {
                return Problem("Entity set 'Contexto.RegistroManutencoes'  is null.");
            }
            var registroManutencao = await _context.RegistroManutencoes.FindAsync(id);
            if (registroManutencao != null)
            {
                _context.RegistroManutencoes.Remove(registroManutencao);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RegistroManutencaoExists(int id)
        {
          return (_context.RegistroManutencoes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
