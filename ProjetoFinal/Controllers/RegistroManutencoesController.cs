using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using ProjetoFinal.Data;
using ProjetoFinal.Models;

namespace ProjetoFinal.Controllers
{
    [Authorize]
    public class RegistroManutencoesController : Controller
    {
        private readonly Contexto _context;

        public RegistroManutencoesController(Contexto context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var contexto = _context.RegistroManutencoes.Include(r => r.Computador).Include(r => r.Procedimento);
            return View(await contexto.ToListAsync());
        }

        [Authorize(Roles = "User, Admin")]
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

        [Authorize(Roles = "User, Admin")]
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

        [Authorize(Roles = "User, Admin")]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RegistroManutViewModel registroManutViewModel)
        {
            if (ModelState.IsValid)
            {
                var registroManutencao = new RegistroManutencao
                {
                    DataManutencao = registroManutViewModel.DataManutencao,
                    DataPrevisao = registroManutViewModel.DataPrevisao,
                    ComputadorId = registroManutViewModel.ComputadorId,
                    ProcedimentoId = registroManutViewModel.ProcedimentoId
                };

                _context.RegistroManutencoes.Add(registroManutencao);
                await _context.SaveChangesAsync(); 

                return RedirectToAction(nameof(Index));
            }

            registroManutViewModel.ComputadorList = _context.Computadores
                .Include(c => c.Marca)
                .Include(c => c.TipoComputador)
                .Include(c => c.Secao)
                .ToList();
            registroManutViewModel.Marcas = _context.Marca.ToList();
            registroManutViewModel.TiposComputador = _context.TipoComputador.ToList();
            registroManutViewModel.Secoes = _context.Secoes.ToList();
            ViewData["ProcedimentoId"] = new SelectList(_context.Procedimentos, "Id", "Descricao", registroManutViewModel.ProcedimentoId);

            return View(registroManutViewModel);
        }

        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registroManutencao = await _context.RegistroManutencoes.FindAsync(id);
            if (registroManutencao == null)
            {
                return NotFound();
            }

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
                RegistroManutencao = registroManutencao
            };

            ViewData["ProcedimentoId"] = new SelectList(_context.Procedimentos, "Id", "Descricao", registroManutencao.ProcedimentoId);
            return View(viewModel);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, RegistroManutViewModel registroManutViewModel)
        {
            if (id != registroManutViewModel.RegistroManutencao.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var registroManutencao = new RegistroManutencao
                    {
                        Id = registroManutViewModel.RegistroManutencao.Id,
                        DataManutencao = registroManutViewModel.DataManutencao,
                        DataPrevisao = registroManutViewModel.DataPrevisao,
                        ComputadorId = registroManutViewModel.ComputadorId,
                        ProcedimentoId = registroManutViewModel.ProcedimentoId
                    };

                    _context.Update(registroManutencao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegistroManutencaoExists(registroManutViewModel.RegistroManutencao.Id))
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

            registroManutViewModel.ComputadorList = _context.Computadores
                .Include(c => c.Marca)
                .Include(c => c.TipoComputador)
                .Include(c => c.Secao)
                .ToList();
            registroManutViewModel.Marcas = _context.Marca.ToList();
            registroManutViewModel.TiposComputador = _context.TipoComputador.ToList();
            registroManutViewModel.Secoes = _context.Secoes.ToList();
            ViewData["ProcedimentoId"] = new SelectList(_context.Procedimentos, "Id", "Descricao", registroManutViewModel.ProcedimentoId);

            return View(registroManutViewModel);
        }



        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
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
