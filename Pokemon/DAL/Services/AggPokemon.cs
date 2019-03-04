using System;
using System.Data.SqlClient;
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
        private static readonly HttpClient APIUrl = new HttpClient()  // Currently only opening a single port with HttpClient. Very slow.
        {                                                             // Look into Uri management: Multi-thread external API GET requests.
            BaseAddress = new Uri("https://pokeapi.co/api/v2/pokemon/")
        };

        public List<Pokedata> AllPokemon
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

        private static List<Pokedata> GetPokemon()
        {
            List<Pokedata> pokeList = new List<Pokedata>();

            IEnumerable<Task<string>> tasks = Utility.GetResultObjects(APIUrl);
            foreach (var t in tasks)
                t.ContinueWith(completed => {
                    switch (completed.Status)
                    {
                        case TaskStatus.RanToCompletion:
                            pokeList.Add(CreatePokemon(JToken.Parse(completed.Result)));
                            break;
                        case TaskStatus.Faulted: break;
                    }
                }, TaskScheduler.Default);

            Task.WaitAll(tasks.ToArray()); // Works... kinda. Final task completes, but requires additional processing.

            return pokeList;

            /*
            IList<Pokedata> pokedatas = pokeList.Select(p => new Pokedata
            {
                PokemonId = (int)p["id"],
                DefaultImage = (string)p["sprites"]["front_default"],
                Name = (string)p["name"],
                Height = (int)p["height"],
                Weight = (int)p["weight"],
                Moves = new JArray(p["moves"].Children()).Select(m => new Move {
                    MoveId = Utility.URLIndex((string)m["move"]["url"], true),
                    Name = (string)m["move"]["name"]
                }).ToList(),
                Abilities = null
            }).ToList();

            return pokedatas.ToList();
            */
        }

        private static Pokedata CreatePokemon(JToken p)
        {
            Pokedata pokemon = new Pokedata
            {
                PokemonId = (int)p["id"],
                DefaultImage = (string)p["sprites"]["front_default"],
                Name = (string)p["name"],
                Height = (int)p["height"],
                Weight = (int)p["weight"],
                Moves = new JArray(p["moves"].Children()).Select(m => new Move
                {
                    MoveId = Utility.URLIndex((string)m["move"]["url"], true),
                    Name = (string)m["move"]["name"]
                }).ToList(),
                Abilities = null
            };

            return pokemon;
        }
    }
}