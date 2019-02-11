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

        public static Int32 GetMax()
        {
            var data = Client.GetStringAsync("").Result;
            return JsonConvert.DeserializeObject<PokemonBase>(data).count;
        }

        private static List<Result> GetPokemon()
        {
            //var data = Client.GetStringAsync("?limit=" + GetMax()).Result;
            var data = Client.GetStringAsync("?limit=5").Result;
            var results = JsonConvert.DeserializeObject<PokemonBase>(data).results;
            
            char[] urlDelims = new char[] { '/' };
            for (int i = 0; i < results.Count; i++)
            {
                var urlArray = results[i].url.Split(urlDelims, StringSplitOptions.RemoveEmptyEntries);
                results[i].ID = Convert.ToInt32(urlArray.Last());

                data = Client.GetStringAsync(urlArray.Last()).Result;
                results[i].Pokedata = JsonConvert.DeserializeObject<Pokedata>(data);

                results[i].Pokedata.moves.ToList().ForEach(s =>
                {
                    s.PokedataID = results[i].ID;
                });
            }
            return results;
        }

        protected override void Seed(PokemonContext context)
        {
            var moves = new List<Moves> { };

            var results = GetPokemon();
            results.ForEach(s => {
                s.Pokedata.moves.ToList().ForEach(m => moves.Add(m));
                s.Pokedata.moves.Clear();
                context.Results.Add(s);
                context.Pokedatas.Add(s.Pokedata);
            });
            context.SaveChanges();
            
            moves.ForEach(s => context.Moves.Add(s));
            context.SaveChanges();
        }
    }
}