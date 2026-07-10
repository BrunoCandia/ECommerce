import { Component, input } from '@angular/core';
import { BasketItemType } from '../../../shared/models/basket';
import { BasketService } from '../../../core/services/basket.service';
import { RouterLink } from '@angular/router';
import { CurrencyPipe } from '@angular/common';
import { MatButton } from '@angular/material/button';
import { MatIcon } from '@angular/material/icon';

@Component({
  selector: 'app-basket-item',
  imports: [RouterLink, CurrencyPipe, MatButton, MatIcon],
  templateUrl: './basket-item.component.html',
  styleUrl: './basket-item.component.scss'
})
export class BasketItemComponent {
  item = input.required<BasketItemType>();

  constructor(private basketService: BasketService) { }

  decrementQuantity() {
    console.log('Decrementing quantity for productId:', this.item().productId);
    this.basketService.removeItemFromBasket(this.item().productId);
  }

  incrementQuantity() {
    console.log('Incrementing quantity for productId:', this.item().productId);
    this.basketService.addItemToBasket(this.item());
  }

  removeItemFromBasket() {
    console.log('Removing item from basket, productId:', this.item().productId);
    this.basketService.removeItemFromBasket(this.item().productId, this.item().quantity);
  }
}
