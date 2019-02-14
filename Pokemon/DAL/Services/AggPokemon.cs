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
        // Look into: http://docs.automapper.org/en/stable/index.html
        // Is this service doing to much?

        private static readonly HttpClient APIUrl = new HttpClient()
        {
            BaseAddress = new Uri("https://pokeapi.co/api/v2/pokemon/")
        };

        // Getting Max number of results
        public static int GetMax()
        {
            var data = APIUrl.GetStringAsync("").Result;
            return JsonConvert.DeserializeObject<PokemonResult>(data).count;
        }

        public static List<Pokedata> getPokemon()
        {
            var APIResult = new API_Pokedata { };
            var PokeList = new List<Pokedata> { };
            char[] urlDelims = new char[] { '/' };

            var poke = new Pokedata { };
            var moves = new Move { };
            
            var data = APIUrl.GetStringAsync("?limit=5").Result;
            JsonConvert.DeserializeObject<PokemonResult>(data).results.ForEach(s => {

                // URL Structure https://pokeapi.co/api/v2/pokemon/ {pokeIndex}
                var pokeIndex = s.url.Split(urlDelims, StringSplitOptions.RemoveEmptyEntries).Last();
                APIResult  = JsonConvert.DeserializeObject<API_Pokedata>(APIUrl.GetStringAsync(pokeIndex).Result);

                poke.PokemonId = APIResult.id;
                poke.Name = APIResult.name;
                poke.DefaultImage = APIResult.sprites.front_default;
                poke.Height = APIResult.height;
                poke.Weight = APIResult.weight;

                poke.Moves = APIResult.moves;





            });

            char[] urlDelims = new char[] { '/' };
            var poke = new Pokedata { };
            for (int i = 0; i < results.Count; i++)
            {
                data = APIUrl.GetStringAsync(results[i].url.Split(urlDelims, StringSplitOptions.RemoveEmptyEntries).Last()).Result;

                poke.PokemonId = Convert.ToInt32(results[i].url.Split(urlDelims, StringSplitOptions.RemoveEmptyEntries).Last());
                poke.Name = results[i].name;
                poke.Height = results[i].



                data = APIUrl.GetStringAsync(urlID).Result;
                pokeList.Add(JsonConvert.DeserializeObject<Pokedata>(data));
                pokeList.Last().moves.ToList().ForEach(s =>
                {
                    s.PokedataID = pokeList.Last().id;
                });

            }
            return pokeList;
        }
    }
}