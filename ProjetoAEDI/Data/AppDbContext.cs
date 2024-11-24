using Microsoft.EntityFrameworkCore;
using ProjetoAEDI.Models;

namespace ProjetoAEDI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Gatinho> Gatinhos { get; set; }
        public DbSet<Adotante> Adotantes { get; set; }
        public DbSet<Adocao> Adocoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurando o relacionamento 1:1 entre Gatinhos e Adoções
            modelBuilder.Entity<Gatinho>()
                .HasOne(g => g.Adocao)
                .WithOne(a => a.Gatinho)
                .HasForeignKey<Adocao>(a => a.GatinhoId);

            // Configurando o relacionamento 1:N entre Adotantes e Adoções
            modelBuilder.Entity<Adotante>()
                .HasMany(a => a.Adocoes)
                .WithOne(ad => ad.Adotante)
                .HasForeignKey(ad => ad.AdotanteId);
        }

    }

}
