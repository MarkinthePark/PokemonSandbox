﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Pokemon.Models
{
    public class PokemonBase
    {
        public int count { get; set; }
        public string next { get; set; }
        public string previous { get; set; }
        public List<Result> results { get; set; }
    }

    public class Result
    {
        [Key]
        public int ID { get; set; }
        public string name { get; set; }
        public string url { get; set; }

        //public virtual ICollection<Pokedata> Pokedatas { get; set; }
    }

}