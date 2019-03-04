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
            IEnumerable<Task<Ability>> moveList =
                from ability in Utility.GetResultObjects(APIUrl)
                select CreateAbility(ability);

            return moveList;
        }

        private static async Task<Ability> CreateAbility(Task<string> fromUrl)
        {

            Ability abilityTask = await fromUrl.ContinueWith(s =>
            {
                JToken a = JToken.Parse(s.Result);
                Ability ability = new Ability
                {
                    AbilityId = (int)a["id"],
                    Name = (string)a["name"]
                };

                return ability;
            }, TaskScheduler.Default);
            
            return abilityTask;
        }
    }
}