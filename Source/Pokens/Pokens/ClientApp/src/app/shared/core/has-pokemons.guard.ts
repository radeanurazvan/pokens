import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { ProfileService } from './profile.service';
import { map } from 'rxjs/operators';

@Injectable()
export class HasPokemonsGuard implements CanActivate {

    constructor(
        private profileService: ProfileService,
        private router: Router
    ) {}

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        return this.profileService.getAllPokemons().pipe(map((data: any) => {
            if (data.length === 0) {
                return true;
            }

            this.router.navigate(['/home']);
            return false;
        }));
    }
}
