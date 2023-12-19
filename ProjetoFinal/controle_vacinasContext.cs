using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ProjetoFinal.Models;

namespace ProjetoFinal
{
    public partial class controle_vacinasContext : DbContext
    {
        public controle_vacinasContext()
        {
        }

        public controle_vacinasContext(DbContextOptions<controle_vacinasContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Endereco> Enderecos { get; set; } = null!;
        public virtual DbSet<SaudePosto> SaudePostos { get; set; } = null!;
        public virtual DbSet<Secretarium> Secretaria { get; set; } = null!;
        public virtual DbSet<Vacina> Vacinas { get; set; } = null!;
        public virtual DbSet<VacinacaoSala> VacinacaoSalas { get; set; } = null!;
        public virtual DbSet<Vacinador> Vacinadors { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=localhost; Initial Catalog = controle_vacinas; User ID=usuarioVacina; password=senha123#; language=Portuguese;Trusted_Connection=True;TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Endereco>(entity =>
            {
                entity.ToTable("endereco");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Bairro)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .HasColumnName("bairro");

                entity.Property(e => e.Cep)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .HasColumnName("cep");

                entity.Property(e => e.Cidade)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .HasColumnName("cidade");

                entity.Property(e => e.Complemento)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .HasColumnName("complemento");

                entity.Property(e => e.Estado)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .HasColumnName("estado");

                entity.Property(e => e.Numero)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("numero");

                entity.Property(e => e.Rua)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .HasColumnName("rua");
            });

            modelBuilder.Entity<SaudePosto>(entity =>
            {
                entity.ToTable("saude_posto");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.FkEndereco).HasColumnName("fk_endereco");

                entity.Property(e => e.FkSecretaria).HasColumnName("fk_secretaria");

                entity.Property(e => e.Nome)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nome");

                entity.Property(e => e.Senha)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("senha");

                entity.Property(e => e.Username)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("username");

                entity.HasOne(d => d.FkEnderecoNavigation)
                    .WithMany(p => p.SaudePostos)
                    .HasForeignKey(d => d.FkEndereco)
                    .HasConstraintName("FK__saude_pos__fk_en__3B75D760");

                entity.HasOne(d => d.FkSecretariaNavigation)
                    .WithMany(p => p.SaudePostos)
                    .HasForeignKey(d => d.FkSecretaria)
                    .HasConstraintName("FK__saude_pos__fk_se__3C69FB99");
            });

            modelBuilder.Entity<Secretarium>(entity =>
            {
                entity.ToTable("secretaria");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Nome)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nome");

                entity.Property(e => e.Senha)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("senha");

                entity.Property(e => e.Username)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<Vacina>(entity =>
            {
                entity.ToTable("vacina");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Descricao)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("descricao");

                entity.Property(e => e.Lote)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("lote");

                entity.Property(e => e.Nome)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("nome");

                entity.Property(e => e.QtdEstoque).HasColumnName("qtd_estoque");

                entity.Property(e => e.Validade)
                    .HasColumnType("date")
                    .HasColumnName("validade");
            });

            modelBuilder.Entity<VacinacaoSala>(entity =>
            {
                entity.ToTable("vacinacao_sala");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.FkPosto).HasColumnName("fk_posto");

                entity.Property(e => e.FkVacina).HasColumnName("fk_vacina");

                entity.Property(e => e.FkVacinador).HasColumnName("fk_vacinador");

                entity.Property(e => e.HorarioAbertura).HasColumnName("horario_abertura");

                entity.Property(e => e.HorarioFechamento).HasColumnName("horario_fechamento");

                entity.Property(e => e.QtdDisponivel).HasColumnName("qtd_disponivel");

                entity.HasOne(d => d.FkPostoNavigation)
                    .WithMany(p => p.VacinacaoSalas)
                    .HasForeignKey(d => d.FkPosto)
                    .HasConstraintName("FK__vacinacao__fk_po__45F365D3");

                entity.HasOne(d => d.FkVacinaNavigation)
                    .WithMany(p => p.VacinacaoSalas)
                    .HasForeignKey(d => d.FkVacina)
                    .HasConstraintName("FK__vacinacao__fk_va__44FF419A");

                entity.HasOne(d => d.FkVacinadorNavigation)
                    .WithMany(p => p.VacinacaoSalas)
                    .HasForeignKey(d => d.FkVacinador)
                    .HasConstraintName("FK__vacinacao__fk_va__440B1D61");
            });

            modelBuilder.Entity<Vacinador>(entity =>
            {
                entity.ToTable("vacinador");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.FkPostoSaude).HasColumnName("fk_posto_saude");

                entity.Property(e => e.Nome)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nome");

                entity.Property(e => e.Senha)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("senha");

                entity.Property(e => e.Username)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("username");

                entity.HasOne(d => d.FkPostoSaudeNavigation)
                    .WithMany(p => p.Vacinadors)
                    .HasForeignKey(d => d.FkPostoSaude)
                    .HasConstraintName("FK__vacinador__fk_po__3F466844");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
