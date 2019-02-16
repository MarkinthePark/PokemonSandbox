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
        
        // Getting Max number of results
        public static int GetMax()
        {
            var data = APIUrl.GetStringAsync("").Result;
            return JsonConvert.DeserializeObject<PokemonResult>(data).count;           // Add GetMax() and GetURLIndex() to Utility doc within same namespace.
        }

        public static int GetURLIndex (string url)
        {
            char[] urlDelims = new char[] { '/' };
            return Convert.ToInt32(url.Split(urlDelims, StringSplitOptions.RemoveEmptyEntries).Last());
        }

        public static List<Pokedata> GetPokemon()
        {
            var APIResult = new API_Pokedata { };
            var PokeList = new List<Pokedata> { };
            
            var data = APIUrl.GetStringAsync("?limit=" + GetMax()).Result;  // Determine number of GET requests to API. Use GetMax() for complete query.
            JsonConvert.DeserializeObject<PokemonResult>(data).results.ForEach(s => {

                // URL Structure https://pokeapi.co/api/v2/pokemon/ {pokeIndex}
                var pokeIndex = GetURLIndex(s.url);
                APIResult  = JsonConvert.DeserializeObject<API_Pokedata>(APIUrl.GetStringAsync(pokeIndex.ToString()).Result);

                // Unconvential and potentially incorrect way of aggregating data from business object to EF model.
                // Look into: http://docs.automapper.org/en/stable/index.html
                var poke = new Pokedata { };

                poke.PokemonId = APIResult.id;
                poke.Name = APIResult.name;
                poke.DefaultImage = APIResult.sprites.front_default;
                poke.Height = APIResult.height;
                poke.Weight = APIResult.weight;

                APIResult.moves.ForEach(m =>
                {
                    var move = new Move { };
                    move.Name = m.move.name;
                    move.MoveId = GetURLIndex(m.move.url);
                    poke.Moves.Add(move);
                });

                APIResult.abilities.ForEach(m =>
                {
                    var abil = new Ability { };
                    abil.Name = m.ability.name;
                    abil.AbilityId = GetURLIndex(m.ability.url);
                    poke.Abilities.Add(abil);
                });

                PokeList.Add(poke);
            });
            
            return PokeList;
        }
    }
}