import { Injectable } from '@angular/core';
import { ShopParams } from '../../shared/models/shopParams';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Pagination } from '../../shared/models/pagination';
import { Product } from '../../shared/models/product';
import { Type } from '../../shared/models/type';
import { Brand } from '../../shared/models/brand';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  
  //baseUrl = 'http://localhost:8000/' //environment.apiUrl;
  baseUrl = 'http://localhost:8000/api/v1/';
  params = new HttpParams();
  
  constructor(private httpClient: HttpClient) { }

  getProducts(shopParams: ShopParams) {
    this.params = new HttpParams();

    if (shopParams.brands && shopParams.brands.length > 0) {
      const brands = shopParams.brands.join(',');
      this.params = this.params.append('brands', brands);      
    }

    if (shopParams.types && shopParams.types.length > 0) {
      const types = shopParams.types.join(',');
      this.params = this.params.append('types', types);
    }

    if (shopParams.sort) {
      this.params = this.params.append('sort', shopParams.sort);
    }

    if (shopParams.search) {
      this.params = this.params.append('search', shopParams.search);
    }

    this.params = this.params.append('pageSize', shopParams.pageSize);
    this.params = this.params.append('pageIndex', shopParams.pageIndex);

    //return this.httpClient.get<Pagination<Product>>(this.baseUrl + 'products', { params: this.params});
    return this.httpClient.get<Pagination<Product>>(this.baseUrl + 'Catalog/GetProductsPaginated?', { params: this.params});
  }

  getProduct(id: string) {
    return this.httpClient.get<Product>(this.baseUrl + 'Catalog/GetProductById/' + id);
  }

  getTypes() {
    //return this.httpClient.get<string[]>(this.baseUrl + 'products/types');
    //return this.httpClient.get<string[]>(this.baseUrl + 'Catalog/GetTypes');
    return this.httpClient.get<Type[]>(this.baseUrl + 'Catalog/GetTypes');
  }

  getBrands() {
    //return this.httpClient.get<string[]>(this.baseUrl + 'products/brands');
    //return this.httpClient.get<string[]>(this.baseUrl + 'Catalog/GetBrands');
    return this.httpClient.get<Brand[]>(this.baseUrl + 'Catalog/GetBrands');
  }  
}
