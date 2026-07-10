import { Component } from '@angular/core';
import { CurrencyPipe, Location } from '@angular/common';
import { BasketService } from '../../../core/services/basket.service';
import { MatIcon } from '@angular/material/icon';
import { MatButton } from '@angular/material/button';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-order-summary',
  imports: [RouterLink, CurrencyPipe, MatButton, MatIcon],
  templateUrl: './order-summary.component.html',
  styleUrl: './order-summary.component.scss'
})
export class OrderSummaryComponent {

  get total() {
    return this.basketService.totals()?.total ?? 0;
  }

  get locationPath() {
    return this.location.path();
  }

  constructor(private basketService: BasketService, private location: Location) { }
}
