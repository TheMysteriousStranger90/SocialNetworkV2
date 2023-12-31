import {Injectable} from '@angular/core';
import {User} from '../shared/models/user';
import {HttpClient} from '@angular/common/http';
import {BehaviorSubject, map, of, ReplaySubject} from 'rxjs';
import {Router} from '@angular/router';
import {PresenceService} from '../core/services/presence.service';
import { environment } from 'environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  baseUrl = environment.apiUrl;
  private currentUserSource = new ReplaySubject<User | null>(1);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient, private presenceService: PresenceService, private router: Router) {
  }

  login(model: any) {
    return this.http.post<User>(this.baseUrl + 'account/login', model).pipe(
      map((response: User) => {
        const user = response;
        if (user) {
          this.setCurrentUser(user);
        }
      })
    )
  }

  register(model: any) {
    return this.http.post<User>(this.baseUrl + 'account/register', model).pipe(
      map(user => {
        if (user) {
          this.setCurrentUser(user);
        }
      })
    )
  }

  logout() {
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
    this.presenceService.stopHubConnection();
    this.router.navigateByUrl('/');
  }

  confirmEmail(userName: string) {
    const token = this.getTokenFromLocalStorage();
    return this.http.post<boolean>(this.baseUrl + 'account/confirm-email', {userName, token});
  }

  resetPassword(userName: string, newPassword: string) {
    const token = this.getTokenFromLocalStorage();
    return this.http.post<boolean>(this.baseUrl + 'account/reset-password', {userName, token, newPassword});
  }

  changePassword(userName: string, currentPassword: string, newPassword: string) {
    const token = this.getTokenFromLocalStorage();
    return this.http.post<boolean>(this.baseUrl + 'account/change-password', {
      userName,
      currentPassword,
      newPassword,
      token
    });
  }

  checkEmailExists(email: string) {
    return this.http.get<boolean>(this.baseUrl + 'account/emailExists?email=' + email);
  }

  checkUsernameExists(username: string) {
    return this.http.get<boolean>(this.baseUrl + 'account/usernameExists?username=' + username)
  }

  setCurrentUser(user: User) {
    user.roles = [];
    const roles = this.getDecodedToken(user.token).role;
    Array.isArray(roles) ? user.roles = roles : user.roles.push(roles);
    localStorage.setItem('user', JSON.stringify(user));
    this.currentUserSource.next(user);
    this.presenceService.createHubConnection(user);
  }

  getDecodedToken(token: string) {
    return JSON.parse(atob(token.split('.')[1]))
  }

  private getTokenFromLocalStorage() {
    const user = JSON.parse(localStorage.getItem('user') || '{}');
    return user.token;
  }
}
