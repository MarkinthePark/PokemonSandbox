using Pokemon.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Pokemon.DAL
{
    public class PokemonContext : DbContext
    {

        public PokemonContext() : base("PokemonContext")
        {
        }
        
        public DbSet<Pokedata> Pokedatas { get; set; }
        public DbSet<Moves> Moves { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}

//https://docs.microsoft.com/en-us/aspnet/mvc/overview/getting-started/getting-started-with-ef-using-mvc/creating-an-entity-framework-data-model-for-an-asp-net-mvc-application