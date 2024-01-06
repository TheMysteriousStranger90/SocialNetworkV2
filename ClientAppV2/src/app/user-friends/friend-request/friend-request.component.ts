import {Component, OnInit} from '@angular/core';
import { MembersService } from 'src/app/members/members.service';
import {ToastrService} from "ngx-toastr";

@Component({
  selector: 'app-friend-request',
  templateUrl: './friend-request.component.html',
  styleUrls: ['./friend-request.component.scss']
})
export class FriendRequestComponent implements OnInit {
  friendRequestsUsers: any;

  constructor(private membersService: MembersService, private toastr: ToastrService) {
  }

  ngOnInit(): void {
    this.loadfriendRequestsUsers();
  }

  loadfriendRequestsUsers() {
    this.membersService.getFriendRequestsByUserName().subscribe({
      next: users => this.friendRequestsUsers = users,
      error: err => console.error(err)
    });
  }

  acceptFriendRequest(friendName: string) {
    this.membersService.acceptFriendRequest(friendName).subscribe({
      next: () => {
        this.toastr.success('Friend request accepted');
        this.loadfriendRequestsUsers();
      },
      error: err => this.toastr.error(err)
    });
  }

  rejectFriendRequest(friendName: string) {
    this.membersService.rejectFriendRequest(friendName).subscribe({
      next: () => {
        this.toastr.success('Friend request rejected');
        this.loadfriendRequestsUsers();
      },
      error: err => this.toastr.error(err)
    });
  }
}


