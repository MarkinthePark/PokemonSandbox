using System;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Pokemon.Models;
using Pokemon.Models.Business;

namespace Pokemon.DAL.Services
{
    public class AggPokemon
    {
        private static readonly HttpClient APIUrl = new HttpClient()
        {
            BaseAddress = new Uri("https://pokeapi.co/api/v2/pokemon/")
        };
        
        public IEnumerable<Task<Pokedata>> AllPokemon
        {
            get
            {
                return GetPokemon();
            }
            set
            {
                AllPokemon = value;
            }
        }

        private static IEnumerable<Task<Pokedata>> GetPokemon()
        {
            IEnumerable<Task<Pokedata>> pokeList =
                from poke in Utility.GetResultObjects(APIUrl)
                select CreatePokemon(poke);
            
            return pokeList;
        }

        private static async Task<Pokedata> CreatePokemon(Task<string> fromUrl)
        {
            Pokedata pokeTask = await fromUrl.ContinueWith(s =>
            {
                PokemonContext db = new PokemonContext();
                JToken p = JToken.Parse(s.Result);
                Pokedata poke = new Pokedata
                {
                    PokemonId = (int)p["id"],
                    DefaultImage = (string)p["sprites"]["front_default"],
                    Name = (string)p["name"],
                    Height = (int)p["height"],
                    Weight = (int)p["weight"],
                    /*
                    Moves = new JArray(p["moves"].Children()).Select(m => new Move  // May want to collect this from DBSet directly to collect existing entity. May cause duplication.
                    {
                        MoveId = Utility.URLIndex((string)m["move"]["url"], true),
                        Name = (string)m["move"]["name"]
                    }).ToList(),

                    Abilities = new JArray(p["abilities"].Children()).Select(a => new Ability
                    {
                        AbilityId = Utility.URLIndex((string)a["ability"]["url"], true),
                        Name = (string)a["ability"]["name"]
                    }).ToList(),
                    */
                    
                    Moves = new JArray(p["moves"].Children()).Select(m =>
                        (Move)DBAttribute(db.Moves, m["move"])
                    ).ToList(),
                    Abilities = new JArray(p["abilities"].Children()).Select(a =>
                        (Ability)DBAttribute(db.Abilities, a["ability"])
                    ).ToList(),
                    
                };

                db.Dispose();
                return poke;

            }, TaskScheduler.Default);

            return pokeTask;
        }

        private static object DBAttribute(DbSet db, JToken attr)
        {
            NamedResource fromJSON = attr.ToObject<NamedResource>();
            int key = Utility.URLIndex(fromJSON.url, true);
            return db.Find(key);
        }
    }
}