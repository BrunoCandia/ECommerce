// Inherited from User model, but also includes the authentication token
import { User } from './user';

export interface UserWithToken extends User {
  token: string;
}