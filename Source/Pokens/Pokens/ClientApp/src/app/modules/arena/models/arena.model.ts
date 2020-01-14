import { TrainerModel } from './trainer.model';

export class ArenaModel {
    id: string;
    name: string;
    requiredLevel: number;
    trainers: TrainerModel[];
}
