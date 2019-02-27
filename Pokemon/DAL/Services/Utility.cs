using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Pokemon.Models.Business;

namespace Pokemon.DAL.Services
{
    public class Utility
    {
        /*
         * The GetObjectResults() returns a complete query of every item in the
         * BaseUri. Making this code as recyleable as possible.
         * 
         * Wondering how secure returning a JArray is.
         * 
         * Every resource Uri is: https://pokeapi.co/api/v2/ {resource} / {index}
         * Should attempt to validate this before release!
         * 
         * Change GetResultObjects into async function.
         */
        public static List<Task<string>> GetResultObjects(HttpClient APIUrl)
        {
            List<Task<string>> resultObjects = new List<Task<string>>();
            foreach (NamedResource result in ResultList(APIUrl))
            {
                string id = URLIndex(result.url);  // Setting to easily append to BaseUri

                Task<string> resultItem = APIUrl.GetStringAsync(id);
                resultObjects.Add(resultItem);
            };


            return resultObjects;
        }

        private static List<NamedResource> ResultList(HttpClient APIUrl)
        {
            int maxCount = Count(APIUrl);

            // Using "count" property in a URL query to get complete list
            string maxResults = APIUrl.GetStringAsync("?limit=" + maxCount).Result;
            List<NamedResource> results = JObject.Parse(maxResults)["results"].ToObject<List<NamedResource>>();
            return results;
        }

        /*
         * Making Count public for running maintenence with TDD
         */
        public static int Count(HttpClient APIUrl)
        {
            // Get "count" property from APIUrl
            string partialResult = APIUrl.GetStringAsync("").Result;
            int count = JObject.Parse(partialResult)["count"].ToObject<int>();
            return count;
        }

        /*
         * Making URLIndex public for use in PK validation in TDD
         */
        public static string URLIndex(string url)
        {
            // Get {index} from Uri
            char[] urlDelims = new char[] { '/' };
            string index = url.Split(urlDelims, StringSplitOptions.RemoveEmptyEntries).Last();
            return index;
        }

        public static int URLIndex(string url, bool toInt)
        {
            // Get {index} from Uri
            char[] urlDelims = new char[] { '/' };
            string index = url.Split(urlDelims, StringSplitOptions.RemoveEmptyEntries).Last();
            return Convert.ToInt32(index);
        }
    }
}