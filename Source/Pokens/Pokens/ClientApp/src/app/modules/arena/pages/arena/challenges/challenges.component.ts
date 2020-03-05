import { Component, OnInit } from "@angular/core";
import { ArenaService } from "../../../core/arena.service";
import { ToastrService } from "../../../../../shared/core/toastr.service";
import { ActivatedRoute, Router } from "@angular/router";
import { map } from 'rxjs/operators';
import { BattlesService } from '../../../core/battles.service';

@Component({
  templateUrl: "./challenges.component.html",
  styleUrls: ["./challenges.component.scss"]
})
export class ChallengesComponent implements OnInit {
  public challenges: any[] = [];
  public sentChallenges: any[] = [];
  public pokemons: any[] = [];
  public hasCurrentBattle = false;

  constructor(
    private service: ArenaService,
    private battlesService: BattlesService,
    private toastr: ToastrService,
    private route: ActivatedRoute,
    private router: Router
  ) { }

  public ngOnInit(): void {
    const arenaId = this.route.snapshot.params["id"];
    this.service
      .getReceivedChallenges()
      .subscribe(c => {
        this.challenges = c.filter(x => x.arenaId === arenaId)
        console.log(this.challenges);
        this.getEnemyPokemons();
      });
    this.battlesService.getCurrentBattle().subscribe(() => {
      this.hasCurrentBattle = true;
      this.goToBattle();
    }, () => {
      this.hasCurrentBattle = false;
    })
  }

  public accept(challenge) {
    this.service
      .acceptChallenge(challenge)
      .subscribe(() => {
        this.toastr.openToastr("You successfully accepted the challenge!");
      }
      );
  }

  public refuse(challenge) {
    // this.service
    //   .refuseChallenge(challenge)
    //   .subscribe(() => {
    this.toastr.openToastr("You refused the challenge :(")
    // });
  }

  public goToBattle() {
    this.router.navigateByUrl('/home/arena/current-battle')
  }

  private findPokemonByTrainer(trainerId: string) {
    return this.pokemons.find(p => p.trainerId === trainerId);
  }

  private getEnemyPokemons() {
    this.challenges.forEach(c => {
      this.service.getAllPokemons([c.enemyId])
        .pipe(
          map(pokemons => pokemons.find(p => p.id === c.enemyPokemonId))
        )
        .subscribe(res => {
          this.pokemons.push(res);
        });
    })
  }
}
