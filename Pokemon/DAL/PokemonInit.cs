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

        public async Task<List<Result>> getPokemon()
        {
            var data = await Client.GetStringAsync("?limit=964");
            var results = JsonConvert.DeserializeObject<PokemonBase>(data).results;
            return results;
        }

        protected override void Seed(PokemonContext context)
        {
            var results = getPokemon().Result;

            results.ForEach(s => context.Results.Add(s));
            context.SaveChanges();
        }
    }
}