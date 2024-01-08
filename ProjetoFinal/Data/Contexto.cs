using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjetoFinal.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace ProjetoFinal.Data
{
    public class Contexto : IdentityDbContext
    {
        public DbSet<Secao> Secoes { get; set; }
        public DbSet<Procedimento> Procedimentos { get; set; }
        public DbSet<Computador> Computadores { get; set; }
        public DbSet<RegistroManutencao> RegistroManutencoes { get; set; }
        public Contexto(DbContextOptions<Contexto> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Computador>()
               .HasOne(c => c.Secao)
               .WithMany(s => s.Computadores)
               .HasForeignKey(c => c.SecaoId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RegistroManutencao>()
                .HasOne(r => r.Computador)
                .WithMany(c => c.Manutencoes)
                .HasForeignKey(r => r.ComputadorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RegistroManutencao>()
                .HasOne(r => r.Procedimento)
                .WithMany(p => p.registroManutencoes)
                .HasForeignKey(r => r.ProcedimentoId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        public DbSet<ProjetoFinal.Models.Marca>? Marca { get; set; }

        public DbSet<ProjetoFinal.Models.TipoComputador>? TipoComputador { get; set; }

    }
}
