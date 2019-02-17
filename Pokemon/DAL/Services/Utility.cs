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
    public class Utility
    {
        public static int GetMax(HttpClient APIUrl)
        {
            var data = APIUrl.GetStringAsync("").Result;
            return JsonConvert.DeserializeObject<PokemonResult>(data).count;
        }

        public static int GetURLIndex(string url)
        {
            char[] urlDelims = new char[] { '/' };
            return Convert.ToInt32(url.Split(urlDelims, StringSplitOptions.RemoveEmptyEntries).Last());
        }
    }
}