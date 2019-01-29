using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Pokemon.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public string Get()
        {
            HttpClient http = new HttpClient();
            http.BaseAddress = new Uri("https://pokeapi.co/api/v2/");
            var data = http.GetAsync("pokemon").Result.Content.ReadAsStringAsync().Result;
            return data;
        }

        // GET api/values/5
        public string Get(string id)
        {
            HttpClient http = new HttpClient();
            http.BaseAddress = new Uri("https://pokeapi.co/api/v2/pokemon/");
            var data = http.GetAsync(id).Result.Content.ReadAsStringAsync().Result;
            return data;
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
