using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pokemon.Models
{
    public class version
    {
        public string name { get; set; }
        public string url { get; set; }

        public virtual Game_indices game_indices { get; set; }
    }
}