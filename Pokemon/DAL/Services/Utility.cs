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
         */
        public static JArray GetResultObjects(HttpClient APIUrl)
        {
            //IList<JObject> resultObjects = new List<JObject>();
            JArray resultObjects = new JArray();
            foreach (JToken result in ResultList(APIUrl))
            {
                NamedResource resourseItem = result.ToObject<NamedResource>();  //Extra steps? Use JToken?
                string id = URLIndex(resourseItem.url);  // Setting to easily append to BaseUri

                JObject resultItem = JObject.Parse(APIUrl.GetStringAsync(id).Result);
                resultObjects.Add(resultItem);
            }

            return resultObjects;
        }

        private static IList<JToken> ResultList(HttpClient APIUrl)
        {
            int maxCount = Count(APIUrl);

            // Using "count" property in a URL query to get complete list
            string maxResults = APIUrl.GetStringAsync("?limit=" + maxCount).Result;
            IList<JToken> results = JObject.Parse(maxResults)["results"].Children().ToList();
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