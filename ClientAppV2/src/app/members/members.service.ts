import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { User } from '../shared/models/user';
import { HttpClient, HttpParams } from '@angular/common/http';
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
    const member = [...this.memberCache.values()]
      .reduce((arr, elem) => arr.concat(elem.result), [])
      .find((member: Member) => member.userName === username);

    if (member) return of(member);

    return this.http.get<Member>(this.baseUrl + 'users/' + username);
  }

  getMembers(userParams: UserParams) {
    const response = this.memberCache.get(Object.values(userParams).join('-'));

    if (response) return of(response);

    let params = new HttpParams();
    params = params.append('pageIndex', userParams.pageNumber);
    params = params.append('pageSize', userParams.pageSize);
    params = params.append('minAge', userParams.minAge);
    params = params.append('maxAge', userParams.maxAge);
    params = params.append('gender', userParams.gender);
    params = params.append('orderBy', userParams.orderBy);
    if (userParams.search && userParams.search.trim() !== '') {
      params = params.append('search', userParams.search);
    }
    params = params.append('currentUsername', this.user?.username || '');

    return getPaginatedResult<Member[]>(this.baseUrl + 'users', params, this.http).pipe(
      map(response => {
        this.memberCache.set(Object.values(userParams).join('-'), response);
        return response;
      })
    )
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





  getFriendsByUserName() {
    if (this.user) {
      return this.http.get<Member[]>(this.baseUrl + 'friends/' + this.user.username);
    } else {
      return throwError('User is not logged in');
    }
  }

  getFriendRequestsByUserName() {
    if (this.user) {
      return this.http.get<Member[]>(this.baseUrl + 'friends/' + this.user.username + '/requests');
    } else {
      return throwError('User is not logged in');
    }
  }

  areUsersFriends(friendName: string) {
    if (this.user) {
      return this.http.get<boolean>(this.baseUrl + 'friends/' + this.user.username + '/are-friends/' + friendName);
    } else {
      return throwError('User is not logged in');
    }
  }

  sendFriendRequest(friendName: string) {
    if (this.user) {
      return this.http.post(this.baseUrl + 'friends/' + this.user.username + '/requests/' + friendName, {});
    } else {
      return throwError('User is not logged in');
    }
  }

  acceptFriendRequest(friendName: string) {
    if (this.user) {
      return this.http.put(this.baseUrl + 'friends/' + this.user.username + '/requests/' + friendName + '/accept', {});
    } else {
      return throwError('User is not logged in');
    }
  }

  rejectFriendRequest(friendName: string) {
    if (this.user) {
      return this.http.put(this.baseUrl + 'friends/' + this.user.username + '/requests/' + friendName + '/reject', {});
    } else {
      return throwError('User is not logged in');
    }
  }

  removeFriend(friendName: string) {
    if (this.user) {
      return this.http.delete(this.baseUrl + 'friends/' + this.user.username + '/friends/' + friendName);
    } else {
      return throwError('User is not logged in');
    }
  }
}
