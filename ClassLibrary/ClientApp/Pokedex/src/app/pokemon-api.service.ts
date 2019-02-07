import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { catchError, map, tap } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { Pokemon } from './models/pokemon';

@Injectable({
  providedIn: 'root'
})
export class PokemonAPIService {

  constructor(private http: HttpClient) { }

  public getPokemon(): Observable<Pokemon> {
    return this.http.get<Pokemon>("http://localhost:63321/api/pokemon/25")
  }
}
