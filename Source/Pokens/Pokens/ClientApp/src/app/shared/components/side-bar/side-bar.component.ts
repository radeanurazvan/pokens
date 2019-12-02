import { Component, OnInit } from '@angular/core';

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

  constructor(private userService: UserService) { }

  public ngOnInit(): void {
    this.setDefaultMenuItems();
    this.setUserName();
  }

  public selectItem(index: number): void {
    this.sideMenuItems.forEach(s => {
      if (s.isSelected) {
        s.isSelected = false;
      }
    });
    this.sideMenuItems[index].isSelected = true;
  }

  private setUserName(): void {
    this.userName = this.userService.getUserName();
  }

  private setDefaultMenuItems(): void {
    this.sideMenuItems = [{
      name: 'Profile',
      route: 'profile',
      isSelected: true
    },
    {
      name: 'Map',
      route: 'map',
      isSelected: false
    },
    {
      name: 'Pokedex',
      route: 'pokedex',
      isSelected: false
    }]
  }

}
