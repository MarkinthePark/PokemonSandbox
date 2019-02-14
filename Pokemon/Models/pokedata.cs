using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pokemon.Models
{

    // Doing it right. Testing the results of Many-to-Many contexts.

    public class Pokedata
    {
        public Pokedata()
        {
            this.Moves = new HashSet<Move>();
        }

        public string DefaultImage { get; set; }
        public string Name { get; set; }
        public int PokemonId { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }

        public virtual ICollection<Move> Moves { get; set; }
    }

    public class Move
    {
        public Move()
        {
            this.Pokemons = new HashSet<Pokedata>();
        }

        public int MoveId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Pokedata> Pokemons { get; set; }
    }

    public class Ability
    {
        public Ability()
        {
            this.Pokemons = new HashSet<Pokedata>();
        }

        public int AbilityId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Pokedata> Pokemons { get; set; }
    }
}