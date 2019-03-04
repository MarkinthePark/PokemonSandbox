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
    public class AggAbilities
    {
        private static readonly HttpClient APIUrl = new HttpClient()
        {
            BaseAddress = new Uri("https://pokeapi.co/api/v2/ability/")
        };

        public IEnumerable<Task<Ability>> AllAbilities
        {
            get
            {
                return GetAbilities();
            }
            set
            {
                AllAbilities = value;
            }
        }

        private static IEnumerable<Task<Ability>> GetAbilities()
        {
            // List<Task<Ability>> moveList = new List<Task<Ability>>();

            IEnumerable<Task<Ability>> moveList =
                from abil in Utility.GetResultObjects(APIUrl)
                select CreateAbility(abil);



            /*
            IEnumerable<Task<string>> tasks = Utility.GetResultObjects(APIUrl);
            foreach (var t in tasks)
                t.ContinueWith(completed => {
                    switch (completed.Status)
                    {
                        case TaskStatus.RanToCompletion:
                            moveList.Add(CreateAbility(JToken.Parse(completed.Result)));
                            break;
                        case TaskStatus.Faulted: break;
                    }
                }, TaskScheduler.Default);

            Task.WaitAll(tasks.ToArray());
            */

            return moveList;
        }

        private static async Task<Ability> CreateAbility(Task<string> fromUrl)
        {

            Ability ability = await fromUrl.ContinueWith(s =>
            {
                JToken a = JToken.Parse(s.Result);
                Ability abil = new Ability
                {
                    AbilityId = (int)a["id"],
                    Name = (string)a["name"]
                };

                return abil;
            }, TaskScheduler.Default);
            
            return ability;
        }
    }
}