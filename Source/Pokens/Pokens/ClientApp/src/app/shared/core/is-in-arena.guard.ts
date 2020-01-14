import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { map } from 'rxjs/operators';
import { ArenaService } from 'src/app/modules/arena/core/arena.service';

@Injectable()
export class IsInArenaGuard implements CanActivate {

    constructor(
        private arenaService: ArenaService,
        private router: Router
    ) {}

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        return this.arenaService.getArenaDetails().pipe(map((data: any) => {
            if (data && data.length > 0) {
                this.router.navigate(['/home/arena/details']);
                return false;
            }

            return true;
        }));
    }
}
