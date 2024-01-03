import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { User } from '../shared/models/user';
import { HttpClient } from '@angular/common/http';
import { AccountService } from '../account/account.service';
import {map, Observable, of, switchMap, take} from 'rxjs';
import { UserParams } from '../shared/models/userParams';
import {getPaginatedResult, getPaginationHeaders } from '../shared/models/paginationHelper';
import { Member } from '../shared/models/member';
import { throwError } from 'rxjs';
import { Photo } from '../shared/models/photo';
import { NotificationService } from '../core/notification/notification.service';

@Injectable({
  providedIn: 'root'
})
export class MembersService {

  baseUrl = environment.apiUrl;
  members: Member[] = [];
  memberCache = new Map();
  user: User | undefined;
  userParams: UserParams | undefined;

  constructor(private http: HttpClient, private accountService: AccountService, private notificationService: NotificationService) {
    this.accountService.currentUser$.pipe(take(1)).subscribe({
      next: user => {
        if (user) {
          this.userParams = new UserParams(user);
          this.user = user;
        }
      }
    })
  }

  getMember(username: string) {
    /*
    const member = this.members.find(x => x.userName === userName)
    if (member !== undefined) return of(member);
    return this.http.get<Member>(this.baseUrl + 'users/' + userName);
    */

    const member = [...this.memberCache.values()]
      .reduce((arr, elem) => arr.concat(elem.result), [])
      .find((member: Member) => member.userName === username);

    if (member) return of(member);

    return this.http.get<Member>(this.baseUrl + 'users/' + username);
  }


  addLike(username: string) {
    return this.http.post(this.baseUrl + 'likes/' + username, {});
  }

  getLikes(predicate: string, pageNumber: number, pageSize: number) {
    let params = getPaginationHeaders(pageNumber, pageSize);
    params = params.append('predicate', predicate);
    return getPaginatedResult<Member[]>(this.baseUrl + 'likes', params, this.http);
  }


  getFollowed() {
    if (this.user) {
      return this.http.get<Member[]>(this.baseUrl + 'follow/' + this.user.username + '/followed');
    } else {
      return throwError('User is not logged in');
    }
  }

  getFollowers() {
    if (this.user) {
      return this.http.get<Member[]>(this.baseUrl + 'follow/' + this.user.username + '/followers');
    } else {
      return throwError('User is not logged in');
    }
  }

  followUser(followedUserName: string) {
    if (this.user) {
      return this.http.post(this.baseUrl + 'follow/' + this.user.username + '/follow/' + followedUserName, {});
    } else {
      return throwError('User is not logged in');
    }
  }

  unfollowUser(followedUserName: string) {
    if (this.user) {
      return this.http.delete(this.baseUrl + 'follow/' + this.user.username + '/unfollow/' + followedUserName);
    } else {
      return throwError('User is not logged in');
    }
  }

  isFollowing(otherUserName: string) {
    if (this.user) {
      return this.http.get<boolean>(this.baseUrl + 'follow/' + this.user.username + '/is-following/' + otherUserName);
    } else {
      return throwError('User is not logged in');
    }
  }


  getUserParams() {
    return this.userParams;
  }

  setUserParams(params: UserParams) {
    this.userParams = params;
  }

  resetUserParams() {
    if (this.user) {
      this.userParams = new UserParams(this.user);
      return this.userParams;
    }
    return;
  }


  setMainPhoto(photoId: number) {
    return this.http.put(this.baseUrl + 'users/set-main-photo/' + photoId, {});
  }

  deletePhoto(photoId: number) {
    return this.http.delete(this.baseUrl + 'users/delete-photo/' + photoId);
  }
}
