using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Pokemon.Models
{
    public class Moves
    {
        [Key]
        public int? ID { get; set; }
        public Move move { get; set; }
        //public List<Version_group_details> version_group_details { get; set; }

        public virtual Pokedata Pokedata { get; set; }
    }

    public class Move
    {
        public string name { get; set; }
        public string url { get; set; }
    }
}