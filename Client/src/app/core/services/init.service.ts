import { Injectable } from '@angular/core';
import { AccountService } from './account.service';

@Injectable({
  providedIn: 'root'
})
export class InitService {

  constructor(private accountService: AccountService) { }

  init() {
    console.log('InitService initialized');

    return this.accountService.getUserInfo();
  }
}
