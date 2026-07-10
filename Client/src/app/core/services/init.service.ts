import { Injectable } from '@angular/core';
import { AccountService } from './account.service';
import { BasketService } from './basket.service';
import { forkJoin, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class InitService {

  constructor(private accountService: AccountService, private basketService: BasketService) { }

  init() {
    console.log('InitService initialized');

    const basket_userName = localStorage.getItem('basket_userName');
    const basket$ = basket_userName ? this.basketService.getBasket(basket_userName) : of(null);

    return forkJoin({
      basket: basket$,
      user: this.accountService.getUserInfo()
    });
  
    //return this.accountService.getUserInfo();
  }
}
