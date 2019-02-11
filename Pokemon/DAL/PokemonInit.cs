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
                    moveArray.Add(s);
                });

                //urlList.Add(urlArray.Last());
            }
            return results;
        }

        private static List<Moves> moveArray = new List<Moves> { };

        /*
        private static List<String> urlList = new List<String> { };

        private static List<Pokedata> GetPokedata()
        {
            var pokeList = new List<Pokedata> { };
            for (int i = 0; i < urlList.Count; i++)
            {
                var data = Client.GetStringAsync(urlList[i]).Result;
                var results = JsonConvert.DeserializeObject<Pokedata>(data);
                pokeList.Add(results);
            }
            return pokeList;
        }
        */

        protected override void Seed(PokemonContext context)
        {
            var results = GetPokemon();
            results.ForEach(s => {
                context.Results.Add(s);
                context.Pokedatas.Add(s.Pokedata);
            });
            context.SaveChanges();

            moveArray.ForEach(s => context.Moves.Add(s));
            context.SaveChanges();

            /*
            var pokedata = GetPokedata();
            pokedata.ForEach(s => context.Pokedatas.Add(s));
            context.SaveChanges();
            */
        }
    }
}