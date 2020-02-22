import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { map, catchError } from 'rxjs/operators';
import { ArenaService } from 'src/app/modules/arena/core/arena.service';
import { of } from 'rxjs';

@Injectable()
export class IsInArenaGuard implements CanActivate {

    constructor(
        private arenaService: ArenaService,
        private router: Router
    ) {}

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        return this.arenaService.getArenaDetails().pipe(
            catchError(() => of(null)),
            map((data: any) => {
                if (data) {
                    this.router.navigate(['/home/arena/details']);
                    return false;
                }

                return true;
            }));
    }
}
