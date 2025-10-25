import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { catchError, throwError } from 'rxjs';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  const router = inject(Router);
  
  return next(req).pipe(
    catchError((err: HttpErrorResponse) => {
      if (err.status === 400) {
        //alert(err.error.title || err.error);
        if (err.error.errors) {
          const modelSateErrors = [];
          for(const key in err.error.errors) {
            if (err.error.errors[key]) {
              modelSateErrors.push(err.error.errors[key]);
            }
          }
          throw modelSateErrors.flat();
        } else {
          //snackbarService.error(err.error.title || err.error);
        }
      }

      if (err.status === 401) {
        //alert(err.error.title || err.error);
        //snackbarService.error(err.error.title || err.error);
        router.navigateByUrl('/not-authenticated');
      }

      // if (err.status === 403) {        
      //   snackbarService.error('Forbidden');
      // }

      if (err.status === 404) {
        router.navigateByUrl('/not-found');
      }

      if (err.status === 500) {
        // const navigationExtras: NavigationExtras = {state: {error: err.error}};
        // router.navigateByUrl('/server-error', navigationExtras);
        router.navigateByUrl('/server-error');
      }

      return throwError(() => err);
    })
  )
};
