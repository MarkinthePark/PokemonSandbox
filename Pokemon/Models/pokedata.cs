using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Pokemon.Models
{

    // Doing it right. Testing the results of Many-to-Many contexts.
    // Using to seed DB. Technically part of the business layer.
    
        
    public class Pokedata
    {
        [Key][DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PokemonId { get; set; }

        public Pokedata()
        {
            this.Moves = new HashSet<Move>();
            this.Abilities = new HashSet<Ability>();
        }

        public string DefaultImage { get; set; }
        public string Name { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }

        public virtual ICollection<Move> Moves { get; set; }
        public virtual ICollection<Ability> Abilities { get; set; }
    }

    public class Move
    {
        public Move()
        {
            this.Pokedatas = new HashSet<Pokedata>();
        }
        
        // Possibly add Generic Id as PK to prevent duplication. -Attempted, created many to one table with only one Pokedata per ICollection
        [Key][DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MoveId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Pokedata> Pokedatas { get; set; }
    }

    public class Ability
    {
        public Ability()
        {
            this.Pokedatas = new HashSet<Pokedata>();
        }

        // AbilityId currently being overwritten by modelBuilder.
        [Key]
        public int AbilityId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Pokedata> Pokedatas { get; set; }
    }
}