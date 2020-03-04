import { Component, OnInit } from "@angular/core";
import { ArenaService } from "../../../core/arena.service";
import { ToastrService } from "../../../../../shared/core/toastr.service";
import { ActivatedRoute } from "@angular/router";

@Component({
  templateUrl: "./challenges.component.html",
  styleUrls: ["./challenges.component.scss"]
})
export class ChallengesComponent implements OnInit {
  public challenges: any[] = [];

  constructor(
    private service: ArenaService,
    private toastr: ToastrService,
    private route: ActivatedRoute
  ) {}

  public ngOnInit(): void {
    const arenaId = this.route.snapshot.params["id"];
    this.service
      .getReceivedChallenges()
      .subscribe(c => (this.challenges = c.filter(x => x.arenaId === arenaId)));
  }

  public accept(challenge) {
    this.service
      .acceptChallenge(challenge)
      .subscribe(() =>
        this.toastr.openToastr("You successfully accepted the challenge!")
      );
  }
}
