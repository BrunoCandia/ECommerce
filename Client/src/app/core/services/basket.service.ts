import { computed, Injectable, signal } from '@angular/core';
import { environment } from '../../../environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { BasketType, BasketItemType, Basket } from '../../shared/models/basket';
import { Product } from '../../shared/models/product';
import { firstValueFrom, map, tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BasketService {
  baseUrl = environment.apiUrl;
  basket = signal<BasketType | null>(null);
  itemCount = computed(() => { 
    return this.basket()?.items.reduce((count, item) => count + item.quantity, 0) ?? 0;
  });
  totals = computed(() => {
    const basket = this.basket();
    
    if (!basket) {
      return null;
    } else {
      const total = basket.items.reduce((sum, item) => sum + (item.price * item.quantity), 0);

      return {
        total
      };
    }
  });

  constructor(private httpClient: HttpClient) { }

  getBasket(userName: string) {
    //return this.httpClient.get<BasketType>(this.baseUrl + 'Basket/GetBasketByUserName?userName=' + userName)  //query string parameter
    return this.httpClient.get<BasketType>(this.baseUrl + 'Basket/GetBasketByUserName/' + userName)             //route parameter
      .pipe(
        map(basket => {
          this.basket.set(basket);
          console.log('GetBasketByUserName retrieved:', basket);
          return basket;
        })
      );
  }

  // getBasket(userName: string) {
  //   userName = 'bruno';
  //   return this.httpClient.get<BasketType>(this.baseUrl + 'Basket/GetBasketByUserName?userName=' + userName).subscribe({
  //     next: basket => this.basket.set(basket),
  //     error: error => console.log(error)
  //   });
  // }

  // This method returns an Observable that the component can subscribe to or use with async/await, 
  // allowing it to react to changes in the basket state.
  setBasket(basket: BasketType) {
    return this.httpClient.post<BasketType>(this.baseUrl + 'Basket/CreateBasket', basket)
      .pipe(
        tap({
          next: basket => this.basket.set(basket),
          error: error => console.log(error)
    }));
  }

  // setBasket(basket: BasketType) {
  //   return this.httpClient.post<BasketType>(this.baseUrl + 'Basket/CreateBasket', basket).subscribe({
  //     next: basket => this.basket.set(basket),
  //     error: error => console.log(error)
  //   });
  // }

  getCurrentBasket() {
    return this.basket();
  }
  
  async addItemToBasket(item: BasketItemType | Product, quantity = 1) {    
    const basket = this.getCurrentBasket() ?? this.createBasket();

    if (this.isProduct(item)) {
      item = this.mapProductItemToBasketItem(item);
    }    
    
    basket.items = this.addOrUpdateItem(basket.items, item, quantity);

    console.log('Call the endpoint for updating basket after adding item:', basket);
    await firstValueFrom(this.setBasket(basket));
  }
  
  async removeItemFromBasket(productId: string, quantity = 1) {
    const basket = this.basket();

    if (!basket) {
      console.log('No basket found');
      return;
    }

    const index = basket.items.findIndex(i => i.productId === productId);

    if (index !== -1) {
      if (basket.items[index].quantity > quantity) {
        basket.items[index].quantity -= quantity;
      } else {
        basket.items.splice(index, 1);
      }

      if (basket.items.length === 0) {
        console.log('Basket is empty, call the endpoint for deleting basket');
        this.deleteBasket();
      } else {
        // TODO: Review this code
        console.log('Call the endpoint for updating basket after removing item:', basket);
        await firstValueFrom(this.setBasket(basket));
      }
    } else {
      console.log('Item not found in basket');
    }
  }

  // TODO: Review if the subscription can be done in the component
  // This method returns an Observable that the component can subscribe to or use with async/await
  deleteBasket() {
    this.httpClient.delete(this.baseUrl + 'Basket/DeleteBasketByUserName/' + this.basket()?.userName).subscribe({
      next: () => {
        localStorage.removeItem('basket_userName');
        this.basket.set(null);
      }
    });
  }
  
  private addOrUpdateItem(items: BasketItemType[], itemToAdd: BasketItemType, quantity: number): BasketItemType[] {
    const item = items.find(i => i.productId === itemToAdd.productId);

    if (item) {
      item.quantity += quantity;
    } else {
      itemToAdd.quantity = quantity;
      items.push(itemToAdd);
    }

    return items;
  }

  private createBasket(): Basket {
    const basket = new Basket();

    // TODO: Get user name from LoggedIn user info, for now hardcoded to 'bruno' for testing
    localStorage.setItem('basket_userName', 'bruno');

    return basket;
  }

  private mapProductItemToBasketItem(item: Product): BasketItemType {
    return {
      productId: item.id,
      productName: item.name,
      productImage: item.imageFile,
      price: item.price,
      quantity: 0
    };
  }

  private isProduct(item: BasketItemType | Product): item is Product {
    return (item as Product).id !== undefined
  }
}