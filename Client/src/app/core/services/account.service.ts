import { Injectable, signal } from '@angular/core';
import { environment } from '../../../environments/environment.development';
import { User } from '../../shared/models/user';
import { HttpClient, HttpParams } from '@angular/common/http';
import { map } from 'rxjs';
import { UserWithToken } from '../../shared/models/userWithToken';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  baseUrl = environment.apiUrl;
  currentUser = signal<User | null>(null);

  constructor(private httpClient: HttpClient) { }

// Identity with SPA: cookie-based authentication and token-based authentication for APIs
// https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity-api-authorization?view=aspnetcore-10.0

  login(values: any) {
    let params = new HttpParams();
    params = params.append('useCookies', false);

    // Use cookie-based authentication when the client is a web application that can use cookies.
    // If useCookies is true, the server will set an HttpOnly cookie with the authentication token, 
    // and the client will include this cookie in subsequent requests to authenticate the user. 
    // This approach is more secure against XSS attacks since the token is not accessible via JavaScript.

    // Use token-based authentication when the client is a mobile app or a third-party application that cannot use cookies, 
    // or when you want to have more control over the authentication process on the client side.
    // If useCookies is false, the server will return the authentication token in the response body, 
    // and the client will need to store this token (e.g., in localStorage) 
    // and include it in the Authorization header of subsequent requests. 
    // This approach is more flexible for APIs that may be consumed by different types of clients 
    // (e.g., mobile apps, third-party applications) that cannot use cookies.
    
    return this.httpClient.post<UserWithToken>(this.baseUrl + 'auth/login', values, {params, withCredentials: true});
    //return this.httpClient.post<User>(this.baseUrl + 'auth/login', values, {params, withCredentials: true});
    // return this.httpClient.post<User>(this.baseUrl + 'login', values, {params, withCredentials: true});
  }

  logout() {
    return this.httpClient.post(this.baseUrl + 'auth/logout', {}, {withCredentials: true});
    // return this.httpClient.post(this.baseUrl + 'account/logout', {}, {withCredentials: true});
  }
  
  register(values: any) {
    return this.httpClient.post(this.baseUrl + 'auth/register', values);
    // return this.httpClient.post(this.baseUrl + 'account/register', values);
  }

  saveToken(token: string) {
    localStorage.setItem('access_token', token);
  }

  getToken(): string | null {
    return localStorage.getItem('access_token');
  }
  
  getUserInfo() {
    console.log('Retrieving user info from url: ', this.baseUrl + 'Auth/user-info');
    return this.httpClient.get<User>(this.baseUrl + 'Auth/user-info')
      .pipe(
          map((user) => {
            console.log('User info retrieved:', user);
            if (user) {              
              this.currentUser.set(user);
              console.log('User info set in currentUser signal:', this.currentUser());
            }
            return user;
          })
        );

    // return this.httpClient.get<User>(this.baseUrl + 'account/user-info')
    //   .pipe(
    //       map((user) => {
    //         if (user) {              
    //           this.currentUser.set(user);
    //         }
    //         return user;
    //       })
    //     );
  }
}
