import {HttpClient} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {environment} from 'src/environments/environment';
import {Notification} from 'src/app/shared/models/notification';
import {Photo} from 'src/app/shared/models/photo';
import {map} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {
  }

  getUnreadCount(userName: string) {
    return this.http.get<Notification[]>(this.baseUrl + 'notifications/' + userName + '/unread').pipe(
      map(notifications => notifications.length)
    );
  }

  getNotifications(userName: string) {
    return this.http.get<Notification[]>(this.baseUrl + 'notifications/' + userName);
  }

  getUnreadNotifications(userName: string) {
    return this.http.get<Notification[]>(this.baseUrl + 'notifications/' + userName + '/unread');
  }

  sendNotification(userName: string, content: string) {
    return this.http.post(this.baseUrl + 'notifications/' + userName, {content});
  }

  markAsRead(userName: string, notificationId: number) {
    return this.http.put(this.baseUrl + 'notifications/' + userName + '/read/' + notificationId, {});
  }

  markAllAsRead(userName: string) {
    return this.http.put(this.baseUrl + 'notifications/' + userName + '/read-all', {});
  }

  createPhotoNotification(photo: Photo) {
    return this.http.post(this.baseUrl + 'notifications/photo-notification/' + photo.id, photo);
  }
}
