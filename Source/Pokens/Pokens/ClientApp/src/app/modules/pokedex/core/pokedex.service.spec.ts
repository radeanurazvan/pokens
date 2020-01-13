import { TestBed } from '@angular/core/testing';

import { PokedexService } from './pokedex.service';

describe('PokedexService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: PokedexService = TestBed.get(PokedexService);
    expect(service).toBeTruthy();
  });
});
