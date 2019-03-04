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

        public IEnumerable<Task<Move>> AllMoves
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

        private static IEnumerable<Task<Move>> GetMoves()
        {
            IEnumerable<Task<Move>> moveList =
                from move in Utility.GetResultObjects(APIUrl)
                select CreateMove(move);

            return moveList;
        }

        private static async Task<Move> CreateMove(Task<string> fromUrl)
        {

            Move moveTask = await fromUrl.ContinueWith(s =>
            {
                JToken m = JToken.Parse(s.Result);
                Move move = new Move
                {
                    MoveId = (int)m["id"],
                    Name = (string)m["name"]
                };

                return move;
            }, TaskScheduler.Default);

            return moveTask;
        }
    }
}