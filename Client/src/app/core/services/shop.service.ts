import { Injectable } from '@angular/core';
import { ShopParams } from '../../shared/models/shopParams';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Pagination } from '../../shared/models/pagination';
import { Product } from '../../shared/models/product';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  
  baseUrl = 'http://localhost:9010/' //environment.apiUrl;
  params = new HttpParams();
  
  constructor(private httpClient: HttpClient) { }

  getProducts(shopParams: ShopParams) {
    this.params = new HttpParams();

    if (shopParams.brands && shopParams.brands.length > 0) {
      this.params = this.params.append('brands', shopParams.brands.join(','));
    }

    if (shopParams.types && shopParams.types.length > 0) {
      this.params = this.params.append('types', shopParams.types.join(','));
    }

    if (shopParams.sort) {
      this.params = this.params.append('sort', shopParams.sort);
    }

    if (shopParams.search) {
      this.params = this.params.append('search', shopParams.search);
    }

    this.params = this.params.append('pageSize', shopParams.pageSize);
    this.params = this.params.append('pageIndex', shopParams.pageIndex);

    return this.httpClient.get<Pagination<Product>>(this.baseUrl + 'products', { params: this.params});
  }

  getProduct(id: string) {
    return this.httpClient.get<Product>(this.baseUrl + 'products/' + id);
  }

  getTypes() {
    return this.httpClient.get<string[]>(this.baseUrl + 'products/types');
  }

  getBrands() {
    return this.httpClient.get<string[]>(this.baseUrl + 'products/brands');
  }  
}
