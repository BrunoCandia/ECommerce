import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AccountService } from '../services/account.service';

export const authGuard: CanActivateFn = (route, state) => {
  const accountService = inject(AccountService);
  const router = inject(Router);

  if (accountService.currentUser()) {
    console.log('User is authenticated, allowing access to route:', state.url);
    return true;
  } else {
    console.log('User is not authenticated, redirecting to not-authenticated page:', state.url);
    router.navigate(['/account/login'], { queryParams: { returnUrl: state.url } });
    return false;
  }
};