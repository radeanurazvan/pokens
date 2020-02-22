import { PokedexStatsModel } from './pokedex-stats.model';
import { PokedexAbilityModel } from './pokedex-ability.model';

export class PokedexPokemonModel {
    public id: string;
    public name: string;
    public stats: PokedexStatsModel;
    public isStarter: boolean;
    public images = [];
    public abilities: PokedexAbilityModel[];

}