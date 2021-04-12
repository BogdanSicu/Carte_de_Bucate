using Carte_de_bucate.Models;
using Microsoft.EntityFrameworkCore;

namespace Carte_de_bucate.Data
{
    public class ReteteContext:DbContext
    {
        public ReteteContext(DbContextOptions<ReteteContext> options):base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IngredienteInRetete>().HasKey(sc => new { sc.MancareID, sc.IngrediendID });

            modelBuilder.Entity<IngredienteInRetete>().HasOne<Mancare>(s => s.Mancare)
           .WithMany(g => g.ListaIngrediente)
           .HasForeignKey(s => s.MancareID);

            modelBuilder.Entity<IngredienteInRetete>().HasOne<Ingrediente>(s => s.Ingredient)
           .WithMany(g => g.ListaRetete)
           .HasForeignKey(s => s.IngrediendID);

            modelBuilder.Entity<Mancare>().HasOne<Tari>(s => s.Tara)
            .WithMany(g => g.ListaMancaruri)
            .HasForeignKey(s => s.TaraID);
        }

        public DbSet<Mancare> Mancare { get; set; }
        public DbSet<Tari> Tari { get; set; }
        public DbSet<Ingrediente> Ingrediente { get; set; }
        public DbSet<IngredienteInRetete> Ingrediente_In_Retete { get; set; }

       
    }
}
