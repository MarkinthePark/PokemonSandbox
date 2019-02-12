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

namespace Pokemon.DAL
{
    public class PokemonInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<PokemonContext>
    {
        private static readonly HttpClient Client = new HttpClient()
        {
            BaseAddress = new Uri("https://pokeapi.co/api/v2/pokemon/")
        };

        public static int GetMax()
        {
            var data = Client.GetStringAsync("").Result;
            return JsonConvert.DeserializeObject<PokemonBase>(data).count;
        }

        private static List<Pokedata> GetPokemon()
        {
            var pokeList = new List<Pokedata> { };

            //var data = Client.GetStringAsync("?limit=" + GetMax()).Result;
            var data = Client.GetStringAsync("?limit=5").Result;
            var results = JsonConvert.DeserializeObject<PokemonBase>(data).results;
            
            char[] urlDelims = new char[] { '/' };
            for (int i = 0; i < results.Count; i++)
            {
                var urlArray = results[i].url.Split(urlDelims, StringSplitOptions.RemoveEmptyEntries);
                var urlID = urlArray.Last();



                data = Client.GetStringAsync(urlID).Result;
                pokeList.Add(JsonConvert.DeserializeObject<Pokedata>(data));
                pokeList.Last().moves.ToList().ForEach(s =>
                {
                    s.PokedataID = pokeList.Last().id;
                });

            }
            return pokeList;
        }

        protected override void Seed(PokemonContext context)
        {
            var moves = new List<Moves> { };

            var pokeList = GetPokemon();
            pokeList.ForEach(s => {
                s.moves.ToList().ForEach(m => moves.Add(m));
                s.moves.Clear();
                context.Pokedatas.Add(s);
            });
            context.SaveChanges();
            
            moves.ForEach(s => context.Moves.Add(s));
            context.SaveChanges();
        }
    }
}