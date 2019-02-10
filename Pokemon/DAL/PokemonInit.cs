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

        private static List<Result> getPokemon()
        {
            var data = Client.GetStringAsync("?limit=964").Result; // Remove hardcoded limit
            var results = JsonConvert.DeserializeObject<PokemonBase>(data).results;
            
            char[] urlDelims = new char[] { '/' };
            for (int i = 0; i < results.Count; i++)
            {
                var urlArray = results[i].url.Split(urlDelims, StringSplitOptions.RemoveEmptyEntries);
                results[i].ID = Convert.ToInt32(urlArray.Last());
            }
            return results;
        }

        protected override void Seed(PokemonContext context)
        {
            var results = getPokemon();
            results.ForEach(s => context.Results.Add(s));
            context.SaveChanges();
        }
    }
}