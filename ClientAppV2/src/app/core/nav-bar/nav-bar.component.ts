import { Component } from '@angular/core';
import { AccountService } from 'src/app/account/account.service';
import { NotificationService } from '../notification/notification.service';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent {
  unreadCount = 0;

  constructor(public accountService: AccountService, private notificationService: NotificationService) {
    this.accountService.currentUser$.subscribe(user => {
      if (user) {
        this.updateUnreadCount(user.username);
      }
    });
  }

  updateUnreadCount(userName: string) {
    this.notificationService.getUnreadCount(userName).subscribe(count => {
      this.unreadCount = count;
    });
  }
}
