using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Pokemon.Models.Business
{
    // *********************************************************************************
    // TO-DO: Abstract from Pokedata to EF in initializer? Or seperate aggregate service?
    // *********************************************************************************

    public class API_Pokedata
    {
        public List<API_Ability> abilities { get; set; }
        public int base_experience { get; set; }
        public List<NamedAPIResource> forms { get; set; }
        public List<API_Game_indices> game_Indices { get; set; }
        public int height { get; set; }
        public List<API_Held_items> held_Items { get; set; }
        public List<API_Moves> moves { get; set; }
        public int id { get; set; }
        public bool is_default { get; set; }
        public string location_area_encounters { get; set; }
        public string name { get; set; }
        public int order { get; set; }
        public NamedAPIResource species { get; set; }
        public API_Sprites sprites { get; set; }
        public List<API_Stats> stats { get; set; }
        public List<API_Types> types { get; set; }
        public int weight { get; set; }
    }

    // Start utility models
    public class NamedAPIResource
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    // Start Abilities model
    public class API_Ability
    {
        public NamedAPIResource ability { get; set; }
        public bool is_hidden { get; set; }
        public int slot { get; set; }
    }

    // Start Game_indices model
    public class API_Game_indices
    {
        public int game_index { get; set; }
        public NamedAPIResource version { get; set; }
    }

    // Start Held_items model
    public class API_Held_items
    {
        public NamedAPIResource item { get; set; }
        public List<API_Version_details> version_Details { get; set; }
    }
    
    public class API_Version_details
    {
        public int rarity { get; set; }
        public NamedAPIResource version { get; set; }
    }
    
    // Start Moves model
    public class API_Moves
    {
        public NamedAPIResource move { get; set; }
        public List<API_Version_group_details> version_group_details { get; set; }
    }

    public class API_Version_group_details
    {
        public int level_learned_at { get; set; }
        public NamedAPIResource move_learn_method { get; set; }
        public NamedAPIResource version_group { get; set; }
    }

    // Start Sprites model
    public class API_Sprites
    {
        public string back_default { get; set; }
        public string back_female { get; set; }
        public string back_shiny { get; set; }
        public string back_shiny_female { get; set; }
        public string front_default { get; set; }
        public string front_female { get; set; }
        public string front_shiny { get; set; }
        public string front_shiny_female { get; set; }
    }

    // Start Stats model
    public class API_Stats
    {
        public int base_stat { get; set; }
        public int effort { get; set; }
        public NamedAPIResource stat { get; set; }
    }

    //Start Types model
    public class API_Types
    {
        public int slot { get; set; }
        public NamedAPIResource type { get; set; }
    }
}