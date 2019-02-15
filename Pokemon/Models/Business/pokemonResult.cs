using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pokemon.Models.Business
{
    // This model will become the list we use to aggregate pokemon catalog

    public class PokemonResult
    {
        public int count { get; set; }
        public string next { get; set; }
        public string previous { get; set; }
        public List<Result> results { get; set; }
    }

    public class Result
    {
        public string name { get; set; }
        public string url { get; set; }
    }

}