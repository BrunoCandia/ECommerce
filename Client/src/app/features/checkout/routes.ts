import { Route } from "@angular/router";
import { CheckoutComponent } from "./checkout.component";
import { authGuard } from "../../core/guards/auth-guard";


export const checkoutRoutes: Route[] = [
  {path: '', component: CheckoutComponent, canActivate: [authGuard]}
];