using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Pokemon.Models;
using Pokemon.Models.Business;

namespace Pokemon.DAL.Services
{
    public class AggMoves
    {
        private static readonly HttpClient APIUrl = new HttpClient()
        {
            BaseAddress = new Uri("https://pokeapi.co/api/v2/move/")
        };

        public List<Move> AllMoves
        {
            get
            {
                return GetMoves();
            }
            set
            {
                AllMoves = value;
            }
        }

        public static List<Move> GetMoves()
        {
            IList<JObject> MoveList = Utility.GetResultObjects(APIUrl);

            // Use Linq to assign MoveList JObjects to _net.Pokedata objects

            /*
             * Changing to fully utilize Newtonsoft JObject
             * Removing redundant API_<Class Objects>
             * 
             * 

            List<Move> MoveList = new List<Move> { };

            string data = APIUrl.GetStringAsync("?limit=" + Utility.GetMax(APIUrl)).Result;
            JsonConvert.DeserializeObject<PokemonResult>(data).results.ForEach(s =>
            {
                Move move = new Move { };
                move.Name = s.name;
                move.MoveId = Utility.GetURLIndex(s.url);
                move.Pokedatas = new List<Pokedata> { };
                MoveList.Add(move);
            });
            */

            return MoveList;
        }
    }
}