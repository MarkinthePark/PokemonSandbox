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

        public static List<Pokedata> GetPokemon()
        {
            JArray pokeList = Utility.GetResultObjects(APIUrl);

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
        }
    }
}