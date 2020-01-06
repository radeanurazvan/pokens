import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { UserService } from '../../core/user.service';
import { SideMenuItem } from '../../models/side-menu-item.model';

@Component({
  selector: 'app-side-bar',
  templateUrl: './side-bar.component.html',
  styleUrls: ['./side-bar.component.scss']
})
export class SideBarComponent implements OnInit {

  sideMenuItems: SideMenuItem[];
  userName: string;

  constructor(
    private userService: UserService,
    private router: Router) { }

  public ngOnInit(): void {
    this.setDefaultMenuItems();
    this.setUserName();
    this.setPreselection();
  }

  public selectItem(index: number): void {
    this.sideMenuItems.forEach(s => {
      if (s.isSelected) {
        s.isSelected = false;
      }
    });
    this.sideMenuItems[index].isSelected = true;
    this.router.navigateByUrl(this.sideMenuItems[index].route);
  }

  private setUserName(): void {
    this.userName = this.userService.getUserName();
  }

  private setDefaultMenuItems(): void {
    this.sideMenuItems = [{
      name: 'Map',
      route: '/home/map',
      isSelected: true
    },
    {
      name: 'Arena',
      route: '/home/arena',
      isSelected: false
    },
    {
      name: 'Profile',
      route: '/home/profile',
      isSelected: false
    },
    {
      name: 'Pokedex',
      route: '/home/pokedex',
      isSelected: false
    }];
  }

  private setPreselection(): void {
    this.sideMenuItems.map(s => {
      if (s.route === this.router.routerState.snapshot.url) {
        s.isSelected = true;
      } else {
        s.isSelected = false;
      }
    });
  }
}
