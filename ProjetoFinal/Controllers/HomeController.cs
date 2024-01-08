using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoFinal.Data;
using ProjetoFinal.Models;
using System;
using System.Diagnostics;
using System.Linq;

namespace ProjetoFinal.Controllers
{
    public class HomeController : Controller
    {
        private readonly Contexto _context;

        public HomeController(Contexto context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            DateTime dataAtual = DateTime.Now;

            int totalComputadores = _context.Computadores.Count();

            ViewBag.TotalComputadoresSemManutencao = _context.Computadores.Count(c => !c.Manutencoes.Any());


            int manutencaoEmDia = _context.Computadores
                .Where(c => c.Manutencoes.Any() && !c.Manutencoes.Any(m => m.DataManutencao.AddMonths(6) < dataAtual))
                .Count();

            int manutencaoVencida = _context.Computadores
                .Where(c => c.Manutencoes.Any() && !c.Manutencoes.Any(m => m.DataManutencao.AddMonths(6) > dataAtual))
                .Count();

            var secoesComMaisComputadoresSemManutencao = _context.Computadores
                .Where(c => c.Manutencoes.Any(m => m.DataManutencao.AddMonths(6) < dataAtual))
                .GroupBy(c => c.SecaoId)
                .Select(g => new
                {
                    SecaoId = g.Key,
                    Quantidade = g.Count()
                })
                .OrderByDescending(g => g.Quantidade)
                .ToList();


            var secoesComMaisComputadoresSemManutencaoComInfo = secoesComMaisComputadoresSemManutencao
                .Join(_context.Secoes,
                    grouped => grouped.SecaoId,
                    secao => secao.Id,
                    (grouped, secao) => new
                    {
                        Secao = secao,
                        Quantidade = grouped.Quantidade
                    })
                .ToList();

            ViewBag.TotalComputadores = totalComputadores;
            ViewBag.ManutencaoEmDia = manutencaoEmDia;
            ViewBag.ManutencaoVencida = manutencaoVencida;
            ViewBag.SecoesComMaisComputadoresSemManutencao = secoesComMaisComputadoresSemManutencaoComInfo;

            return View();
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
