import { Component } from '@angular/core';
import { AdminService } from '../admin.service';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-lock-unlock',
  templateUrl: './lock-unlock.component.html',
  styleUrls: ['./lock-unlock.component.scss']
})
export class LockUnlockComponent {
  userName: string = '';

  constructor(private adminService: AdminService, private snackBar: MatSnackBar) { }

  lockUser() {
    this.adminService.lockUser(this.userName).subscribe({
      next: response => {
        console.log(response);
        this.snackBar.open('User locked successfully', 'Close', { duration: 3000 });
      },
      error: error => {
        console.error(error);
        this.snackBar.open('Failed to lock user', 'Close', { duration: 3000 });
      }
    });
  }

  unlockUser() {
    this.adminService.unlockUser(this.userName).subscribe({
      next: response => {
        console.log(response);
        this.snackBar.open('User unlocked successfully', 'Close', { duration: 3000 });
      },
      error: error => {
        console.error(error);
        this.snackBar.open('Failed to unlock user', 'Close', { duration: 3000 });
      }
    });
  }
}
