using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pokemon.Models
{
    public class Game_indices
    {
        public int game_index { get; set; }
        public ItemVersion version { get; set; }

        public virtual Pokedata Pokedata { get; set; }
    }

    public class ItemVersion
    {
        public string name { get; set; }
        public string url { get; set; }
    }
}