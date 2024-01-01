import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Notification } from 'src/app/shared/models/notification';
@Injectable({
  providedIn: 'root'
})
export class NotificationService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getUnreadCount(userName: string) {
    return this.http.get<number>(this.baseUrl + 'notifications/' + userName + '/unread-count');
  }

  getNotifications(userName: string) {
    return this.http.get<Notification[]>(this.baseUrl + 'notifications/' + userName);
  }

  getUnreadNotifications(userName: string) {
    return this.http.get<Notification[]>(this.baseUrl + 'notifications/' + userName + '/unread');
  }

  sendNotification(userName: string, content: string) {
    return this.http.post(this.baseUrl + 'notifications/' + userName, { content });
  }

  markAsRead(userName: string, notificationId: number) {
    return this.http.put(this.baseUrl + 'notifications/' + userName + '/read/' + notificationId, {});
  }

  markAllAsRead(userName: string) {
    return this.http.put(this.baseUrl + 'notifications/' + userName + '/read-all', {});
  }
}
