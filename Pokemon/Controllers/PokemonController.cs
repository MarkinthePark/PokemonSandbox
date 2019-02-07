using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Newtonsoft.Json;
using Pokemon.Models;


namespace Pokemon.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: ".")]
    public class PokemonController : ApiController
    {

        private static readonly HttpClient Client = new HttpClient()
        {
            BaseAddress = new Uri("https://pokeapi.co/api/v2/pokemon/")
        };


        // GET api/values
        public async Task<List<Results>> Get()
        {
            //Client.BaseAddress = new Uri("https://pokeapi.co/api/v2/");
            var data = await Client.GetStringAsync("?limit=964");
            //var results = JsonConvert.DeserializeObject<PokemonBase>(data).results;
            return JsonConvert.DeserializeObject<PokemonBase>(data).results;
        }

        public void InsertToDB(Array data)
        {
            string connectionString = GetConnectionString();
        }

        // GET api/values/5
        public async Task<Pokedata> Get(string id)
        {
            //Client.BaseAddress = new Uri("https://pokeapi.co/api/v2/pokemon/");
            var data = await Client.GetStringAsync(id);
            return JsonConvert.DeserializeObject<Pokedata>(data);
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
