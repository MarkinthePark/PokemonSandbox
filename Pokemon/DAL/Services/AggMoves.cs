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
            JArray MoveList = Utility.GetResultObjects(APIUrl);

            IList<Move> Moves = MoveList.Select(m => new Move
            {
                MoveId = (int)m["id"],
                Name = (string)m["name"]
            }).ToList();

            return Moves.ToList();
        }
    }
}