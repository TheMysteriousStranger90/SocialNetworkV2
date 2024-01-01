import { Component } from '@angular/core';
import { NotificationService } from './notification.service';
import { AccountService } from 'src/app/account/account.service';
import { Notification } from 'src/app/shared/models/notification';

@Component({
  selector: 'app-notification',
  templateUrl: './notification.component.html',
  styleUrls: ['./notification.component.scss']
})
export class NotificationComponent {
  unreadCount = 0;
  notifications: Notification[] = [];

  constructor(private notificationService: NotificationService, public accountService: AccountService) { }

  ngOnInit() {
    this.accountService.currentUser$.subscribe(user => {
      if (user) {
        this.updateUnreadCount(user.username);
        this.getNotifications(user.username);
      }
    });
  }

  updateUnreadCount(userName: string) {
    this.notificationService.getUnreadCount(userName).subscribe(count => {
      this.unreadCount = count;
    });
  }

  getNotifications(userName: string) {
    this.notificationService.getNotifications(userName).subscribe(notifications => {
      this.notifications = notifications;
    });
  }

  markAsRead(notification: Notification) {
    this.notificationService.markAsRead(notification.username, notification.id).subscribe(() => {
      notification.isRead = true;
      this.updateUnreadCount(notification.username);
    });
  }
}
