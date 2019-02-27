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

        private static List<Move> GetMoves()
        {
            List<Move> moveList = new List<Move>();

            List<Task<string>> tasks = Utility.GetResultObjects(APIUrl);
            foreach (var t in tasks)
                t.ContinueWith(completed => {
                    switch (completed.Status)
                    {
                        case TaskStatus.RanToCompletion:
                            moveList.Add(CreateMove(JToken.Parse(completed.Result)));
                            break;
                        case TaskStatus.Faulted: break;
                    }
                }, TaskScheduler.Default);

            Task.WaitAll(tasks.ToArray());

            return moveList;

            /*
            JArray MoveList = Utility.GetResultObjects(APIUrl);

            IList<Move> Moves = MoveList.Select(m => new Move
            {
                MoveId = (int)m["id"],
                Name = (string)m["name"]
            }).ToList();

            return Moves.ToList();
            */
        }

        private static Move CreateMove(JToken m)
        {
            Move move = new Move
            {
                MoveId = (int)m["id"],
                Name = (string)m["name"]
            };

            return move;
        }
    }
}