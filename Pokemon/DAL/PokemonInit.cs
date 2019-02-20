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

using System.Data.Entity.Migrations; //Test

namespace Pokemon.DAL
{
    public class PokemonInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<PokemonContext>
    {
        protected override void Seed(PokemonContext context)
        {
            /*
            AggMoves MoveList = new AggMoves();
            
            MoveList.AllMoves.ForEach(s =>
            {
                context.Moves.Add(s);
            });
            context.SaveChanges();
            */

            AggPokemon PokeList = new AggPokemon();


            PokeList.AllPokemon.ForEach(p =>
            {
                context.Moves.AddOrUpdate(
                    m => m.Name,
                    p.Moves.ToArray()
                );
                
                context.SaveChanges();

            });

            /*
            MoveList.AllMoves.ForEach(m =>
            {
                context.Pokedatas.AddOrUpdate(
                    p => p.Name,
                    m.Pokedatas.ToArray()
                );

                context.SaveChanges();
            });
            */
        }
    }
}