import { Routes } from '@angular/router';
import { HomeComponent } from './features/home/home.component';
import { ShopComponent } from './features/shop/shop.component';
import { ProductDetailsComponent } from './features/shop/product-details/product-details.component';
import { NotFoundComponent } from './shared/components/not-found/not-found.component';
import { ServerErrorComponent } from './shared/components/server-error/server-error.component';
import { NotAuthenticatedComponent } from './shared/components/not-authenticated/not-authenticated.component';
import { BasketComponent } from './features/basket/basket.component';

export const routes: Routes = [
    {path: '', component: HomeComponent},
    {path: 'shop', component: ShopComponent},
    {path: 'shop/:id', component: ProductDetailsComponent},
    {path: 'shopping-cart', component: BasketComponent},
    // {path: 'basket', component: BasketComponent},
    {path: 'checkout', loadChildren: () => import('./features/checkout/routes').then(routes => routes.checkoutRoutes)},
    {path: 'account', loadChildren: () => import('./features/account/routes').then(routes => routes.accountRoutes)},
    // {path: 'account/login', component: LoginComponent},
    // {path: 'account/register', component: RegisterComponent},
    {path: 'not-authenticated', component: NotAuthenticatedComponent},
    {path: 'not-found', component: NotFoundComponent},
    {path: 'server-error', component: ServerErrorComponent},
    {path: '**', redirectTo: 'not-found', pathMatch: 'full'},
];
