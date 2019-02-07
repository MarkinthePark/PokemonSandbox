import { Component, OnInit } from '@angular/core';
import { Pokemon } from '../models/pokemon';
import { PokemonAPIService } from '../pokemon-api.service';

@Component({
  selector: 'app-pokemon',
  templateUrl: './pokemon.component.html',
  styleUrls: ['./pokemon.component.css']
})
export class PokemonComponent implements OnInit {

  constructor(private pokemonAPIService: PokemonAPIService) { }

  ngOnInit() {
    this.getPokemon()
  }

  getPokemon(): void {
    this.pokemonAPIService.getPokemon()
      .subscribe(pokemon => {
        console.log(pokemon)
      })
  }

}
