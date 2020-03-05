import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { map, catchError } from 'rxjs/operators';
import { of } from 'rxjs';
import { BattlesService } from '../../modules/arena/core/battles.service';

@Injectable({
  providedIn: 'root'
})
export class CurrentBattleGuard implements CanActivate {

    constructor(
        private service: BattlesService,
        private router: Router
    ) {}

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        return this.service.getCurrentBattle().pipe(
            catchError(() => of(null)),
            map((data: any) => {
                if (data) {
                    return true;
                }

                this.router.navigate(['/home/arena']);
                return false;
            }));
    }
}
