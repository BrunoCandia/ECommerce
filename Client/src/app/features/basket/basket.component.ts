import { Component } from '@angular/core';
import { BasketService } from '../../core/services/basket.service';
import { Router } from '@angular/router';
import { BasketItemComponent } from './basket-item/basket-item.component';
import { EmptyStateComponent } from '../../shared/components/empty-state/empty-state.component';
import { BasketItemType } from '../../shared/models/basket';
import { OrderSummaryComponent } from '../../shared/components/order-summary/order-summary.component';

@Component({
  selector: 'app-basket',
  imports: [BasketItemComponent, EmptyStateComponent, OrderSummaryComponent],
  templateUrl: './basket.component.html',
  styleUrl: './basket.component.scss'
})
export class BasketComponent {

  get items(): BasketItemType[] {
    return this.basketService.getCurrentBasket()?.items ?? [];
  }

  constructor(private basketService: BasketService, private router: Router) { }

  navigate() {
    this.router.navigateByUrl('/shop');
  }
}
