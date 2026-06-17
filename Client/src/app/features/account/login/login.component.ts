import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { MatButton } from '@angular/material/button';
import { MatCard } from '@angular/material/card';
import { MatFormField, MatInput, MatLabel } from '@angular/material/input';
import { ActivatedRoute, Router } from '@angular/router';
import { AccountService } from '../../../core/services/account.service';
import { switchMap } from 'rxjs';
import { User } from '../../../shared/models/user';

@Component({
  selector: 'app-login',
  imports: [ReactiveFormsModule, MatCard, MatFormField, MatInput, MatLabel, MatButton],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent implements OnInit {

  loginForm!: FormGroup;
  returnUrl: string = '/shop';

  constructor(
    private fb: FormBuilder, 
    private accountService: AccountService, 
    private router: Router, 
    private activatedRoute: ActivatedRoute) {
      
    const url = this.activatedRoute.snapshot.queryParams['returnUrl'];

    if (url) {
      this.returnUrl = url;
    }
  }

  ngOnInit(): void {
      this.loginForm = this.fb.group({
        email: [''],
        password: ['']
    });
  }

  onSubmit() {
    this.accountService.login(this.loginForm.value).subscribe({
      next: (response) => {
        this.accountService.saveToken(response.token);
        this.accountService.getUserInfo().subscribe({
          next: () => {
            this.router.navigateByUrl('/shop');
          },
          error: error => console.log(error)  
        });        
      }
    });
  }

  // onSubmit() {
  //   this.accountService.login(this.loginForm.value)
  //     .pipe(
  //       switchMap(() => this.accountService.getUserInfo()))
  //     .subscribe({
  //       next: () => this.router.navigateByUrl(this.returnUrl),
  //       // next: () => this.router.navigateByUrl('/shop'),
  //       error: error => console.log(error)
  //   });
  // }

  // onSubmit() {
  //   this.accountService.login(this.loginForm.value).subscribe({
  //     next: () => {
  //       this.accountService.getUserInfo().subscribe({
  //         next: () => {
  //           this.router.navigateByUrl('/shop');
  //         },
  //         error: error => console.log(error)  
  //       });        
  //     }
  //   });
  // }

  // onSubmit() {
  //   this.accountService.login(this.loginForm.value).subscribe({
  //     next: () => {
  //       this.accountService.getUserInfo().subscribe();
  //       this.router.navigateByUrl('/shop');
  //     }
  //   });
  //}

}
