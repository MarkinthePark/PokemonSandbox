﻿using System;
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

            AggMoves MoveList = new AggMoves();
            context.Moves.AddRange(MoveList.AllMoves);
            context.SaveChanges();

            AggPokemon PokeList = new AggPokemon();

            // ***************************************************************
            // This takes roughly 20 minutes to complile the complete database
            // with Moves being the only many to many table.  Make faster!
            // ***************************************************************

            PokeList.AllPokemon.ForEach(p =>
            {
                List<Move> PokeMoves = p.Moves.ToList();
                p.Moves.Clear();
                context.Pokedatas.Add(p);

                context.SaveChanges();

                var existingPokemon = context.Pokedatas.Include("Moves")
                    .Where(ep => ep.Name == p.Name).FirstOrDefault<Pokedata>();


                // Make a function out of this.
                foreach (Move m in PokeMoves)
                {
                    // Look into bulk find method to match class object with existing entities

                    var existingMove = context.Moves.Find(m.MoveId);
                    existingPokemon.Moves.Add(existingMove);
                }
                context.SaveChanges();
            });
        }
    }
}