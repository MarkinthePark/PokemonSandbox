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

using System.Data;
using System.Data.Entity.Migrations;

namespace Pokemon.DAL
{
    public class PokemonInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<PokemonContext>
    {
        protected override void Seed(PokemonContext context)
        {
            AggAbilities AbilList = new AggAbilities();

            foreach (var a in AbilList.AllAbilities)
                a.ContinueWith(completed => {
                    switch (completed.Status)
                    {
                        case TaskStatus.RanToCompletion:
                            PokemonContext ability = new PokemonContext();
                            ability.Abilities.Add(completed.Result);
                            ability.SaveChanges();
                            break;
                        case TaskStatus.Faulted: break;
                    }
                }, TaskScheduler.Default);

            








            /*
            AggMoves MoveList = new AggMoves();
            context.Moves.AddRange(MoveList.AllMoves);
            context.SaveChanges();

            AggPokemon PokeList = new AggPokemon();

            // ***************************************************************
            // This takes roughly 20 minutes to complile the complete database
            // with Moves being the only   to many table.  Make faster!
            //
            // Now it uses 2GB of data to init... greatttttttttt.
            // Handle speed with ASync?
            // Find source of memory leak. Likely JObject cleanup.
            // ***************************************************************

            PokeList.AllPokemon.ForEach(p =>
            {
                List<Move> PokeMoves = p.Moves.ToList();
                p.Moves.Clear();
                context.Pokedatas.Add(p);

                context.SaveChanges();

                var existingPokemon = context.Pokedatas.Include("Moves")
                    .Where(ep => ep.Name == p.Name).FirstOrDefault<Pokedata>();


                // Make a function out of this. Also... this is very slow.
                foreach (Move m in PokeMoves)
                {
                    // Look into bulk find method to match class object with existing entities

                    var existingMove = context.Moves.Find(m.MoveId);
                    existingPokemon.Moves.Add(existingMove);
                }
                context.SaveChanges();
            });
            */
        }
    }
}