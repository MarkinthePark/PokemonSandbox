import { TestBed } from '@angular/core/testing';

import { PokemonAPIService } from './pokemon-api.service';

describe('PokemonAPIService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: PokemonAPIService = TestBed.get(PokemonAPIService);
    expect(service).toBeTruthy();
  });
});
