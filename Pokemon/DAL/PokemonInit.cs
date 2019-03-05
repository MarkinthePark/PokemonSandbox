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
            /*
             * This is working and is significantly faster than previous attempts.
             * 
             * Continuing with this pattern will lead to highly readable, yet 
             * very repetitive code.
             * 
             * TODO:
             *   - Implement attribute writes to DBContext as a function. Perform this in Parallel
             *     to allow for abilityDB and moveDB to run async.
             *   - Fill out the body of the application. Give it some personallity.
             *   - Look into VS 2017 event tracking. No longer provides accurate representation.
             */

            AggAbilities AbilList = new AggAbilities();
            foreach (Task<Ability> a in AbilList.AllAbilities)
                a.ContinueWith(completed => {
                    switch (completed.Status)
                    {
                        case TaskStatus.RanToCompletion:
                            PokemonContext abilityDB = new PokemonContext();
                            abilityDB.Abilities.Add(completed.Result);
                            abilityDB.SaveChanges();
                            break;
                        case TaskStatus.Faulted: break;
                    }
                }, TaskScheduler.Default);

            AggMoves MoveList = new AggMoves();
            foreach (Task<Move> m in MoveList.AllMoves)
                m.ContinueWith(completed => {
                    switch (completed.Status)
                    {
                        case TaskStatus.RanToCompletion:
                            PokemonContext moveDB = new PokemonContext();
                            moveDB.Moves.Add(completed.Result);
                            moveDB.SaveChanges();
                            break;
                        case TaskStatus.Faulted: break;
                    }
                }, TaskScheduler.Default);
            
            AggPokemon PokeList = new AggPokemon();
            TaskScheduler scheduler = new ConcurrentExclusiveSchedulerPair().ExclusiveScheduler;
            foreach (Task<Pokedata> p in PokeList.AllPokemon)
                p.ContinueWith(completed => {
                    switch (completed.Status)
                    {
                        case TaskStatus.RanToCompletion:
                            PokemonContext pokemonDB = new PokemonContext();
                            Pokedata pokedata = completed.Result;
                            pokemonDB.Pokedatas.Add(pokedata);
                            pokemonDB.SaveChanges();
                            /*
                            List<Ability> PokeAbils = pokedata.Abilities.ToList();
                            pokedata.Abilities.Clear();
                            List<Move> PokeMoves = pokedata.Moves.ToList();
                            pokedata.Moves.Clear();

                            pokemonDB.Pokedatas.Add(completed.Result);
                            pokemonDB.SaveChanges();

                            Pokedata currentPokemon = pokemonDB.Pokedatas.Find(pokedata.PokemonId);

                            foreach (Ability a in PokeAbils)
                            {
                                currentPokemon.Abilities.Add(a);
                            }

                            foreach (Move m in PokeMoves)
                            {
                                currentPokemon.Moves.Add(m);
                            }
                            */

                            break;
                        case TaskStatus.Faulted: break;
                    }
                }, scheduler);
        }
    }
}