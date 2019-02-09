using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pokemon.Models
{
    public class PokemonBase
    {
        public int count { get; set; }
        public string next { get; set; }
        public string previous { get; set; }
        public List<Results> results { get; set; }
    }

    public class Results
    {
        int _ID;
        public int ID
        {
            get
            {
                //Is this best practice?
                if ( _ID == 0 )
                {
                    char[] urlDelims = new char[] { '/' };
                    string[] urlArray = url.Split(urlDelims, StringSplitOptions.RemoveEmptyEntries);
                    return Convert.ToInt32(urlArray.Last());
                } else
                {
                    return _ID;
                }
            }
            set { _ID = value; }
        }
        public string name { get; set; }
        public string url { get; set; }

        public virtual ICollection<Pokedata> Pokedatas { get; set; }
    }

}