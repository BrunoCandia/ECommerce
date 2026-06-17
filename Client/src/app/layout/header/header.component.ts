import { Component, OnInit } from '@angular/core';
import { MatIcon } from '@angular/material/icon';
import { Router, RouterLink, RouterLinkActive, RouterModule } from '@angular/router';
import { BusyService } from '../../core/services/busy.service';
import { MatProgressBar } from '@angular/material/progress-bar';
import { AccountService } from '../../core/services/account.service';
import { MatMenu, MatMenuItem, MatMenuTrigger } from '@angular/material/menu';
import { MatDivider } from '@angular/material/divider';
import { MatButton } from '@angular/material/button';
import { MatBadge } from '@angular/material/badge';

@Component({
  selector: 'app-header',
    imports: [MatIcon, MatButton, MatBadge, RouterLink, RouterLinkActive, MatProgressBar, MatMenuTrigger, MatMenu, MatDivider, MatMenuItem],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent implements OnInit {

  ngOnInit(): void {

    // Logic moved to InitService to load the user info when the app initializes, 
    // so the user info is available in the header component and other components 
    // that need it without having to wait for the login component to load and call getUserInfo() after login.

    // To test if the user info is loaded in the header component after login  
    // this.accountService.getUserInfo().subscribe({
    //   next: (user) => {console.log('User info loaded in header component:', user)},
    //   error: err => console.log(err)
    // });
  }

  get isLoading() {
    return this.busyService.loading();
  }

  get currentUser() {
    return this.accountService.currentUser();
  }

  constructor(
    private busyService: BusyService, 
    private accountService: AccountService,
    private router: Router) { }

  logout() {
    this.accountService.logout().subscribe({
      next: () => {
        this.accountService.currentUser.set(null);
        this.router.navigateByUrl('/');
      }
    });
  }

}
