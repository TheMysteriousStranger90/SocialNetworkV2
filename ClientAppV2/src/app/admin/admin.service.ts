import { Injectable } from '@angular/core';
import { User } from '../shared/models/user';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Photo } from '../shared/models/photo';

@Injectable({
  providedIn: 'root'
})
export class AdminService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {
  }

  getUsersWithRoles() {
    return this.http.get<User[]>(this.baseUrl + 'admin/users-with-roles');
  }

  updateUserRoles(username: string, roles: string[]) {
    return this.http.post<string[]>(this.baseUrl + 'admin/edit-roles/'
      + username + '?roles=' + roles, {});
  }

  getRoles() {
    return this.http.get<string[]>(this.baseUrl + 'admin/roles');
  }

  addRole(roleName: string) {
    return this.http.post(this.baseUrl + 'admin/add-role', { roleName });
  }

  deleteRole(roleName: string) {
    return this.http.delete(this.baseUrl + 'admin/delete-role/' + roleName);
  }

  getPhotosForApproval() {
    return this.http.get<Photo[]>(this.baseUrl + 'admin/photos-to-moderate');
  }

  approvePhoto(photoId: number) {
    return this.http.post(this.baseUrl + 'admin/approve-photo/' + photoId, {});
  }

  rejectPhoto(photoId: number) {
    return this.http.post(this.baseUrl + 'admin/reject-photo/' + photoId, {});
  }

  lockUser(userName: string) {
    return this.http.post(this.baseUrl + 'admin/lock-user/' + userName, {});
  }

  unlockUser(userName: string) {
    return this.http.post(this.baseUrl + 'admin/unlock-user/' + userName, {});
  }
}

