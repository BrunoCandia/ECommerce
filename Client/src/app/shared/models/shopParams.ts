import { Brand } from "./brand";
import { Type } from "./type";

export class ShopParams {
    brands: Brand[] = [];
    types: Type[] = [];
    // brands: string[] = [];
    // types: string[] = [];
    sort: string = 'name';
    pageIndex: number = 1;
    pageSize: number = 10;
    search: string = '';
}