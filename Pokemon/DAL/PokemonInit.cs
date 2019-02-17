using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json;
using Pokemon.Models;
using Pokemon.DAL.Services;

namespace Pokemon.DAL
{
    public class PokemonInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<PokemonContext>
    {
        protected override void Seed(PokemonContext context)
        {
            var MoveList = new AggMoves();
            MoveList.AllMoves.ForEach(s =>
            {
                context.Moves.Add(s);
            });
            context.SaveChanges();

            var PokeList = new AggPokemon();
            PokeList.AllPokemon.ForEach(s =>
            {
                context.Pokedatas.Add(s);
            });
            context.SaveChanges();
        }
    }
}