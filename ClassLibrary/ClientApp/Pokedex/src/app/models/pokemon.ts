export class Pokemon {
  abilities: Abilities[];
  base_experiance: number;
  /*
  forms: Forms[];
  game_indices: Game_indices[];
  height: number;
  held_items: Held_items[];
  id: number;
  is_default: boolean;
  location_area_encounters: string;
  moves: Moves[];
  name: string;
  order: number;
  species: Species;
  sprites: Sprites;
  stats: Stats[];
  types: Types[];
  weight: number;
  */
}



//Abilities definition
export class Abilities {
  ability: Ability;
  is_hidden: boolean;
  slot: number;
}

export class Ability {
  name: string;
  url: string;    
}
//-----------------------------
