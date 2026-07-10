export type BasketType = {
    userName: string;
    items: BasketItemType[];
    totalPrice: number;
}

export type BasketItemType = {    
    quantity: number;
    price: number;
    productId: string;
    productName: string;
    productImage: string;
}

export class Basket implements BasketType {    
    userName: string = 'bruno';
    totalPrice: number = 0;
    items: BasketItemType[] = [];    
}