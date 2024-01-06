import {Component, OnInit} from '@angular/core';
import {ToastrService} from "ngx-toastr";
import { MembersService } from 'src/app/members/members.service';

@Component({
  selector: 'app-friend-list',
  templateUrl: './friend-list.component.html',
  styleUrls: ['./friend-list.component.scss']
})
export class FriendListComponent implements OnInit {
  friendsListUsers: string[] | undefined;

  constructor(private membersService: MembersService, private toastr: ToastrService) {
  }

  ngOnInit(): void {
    this.loadfriendListUsers();
  }

  loadfriendListUsers() {
    this.membersService.getFriendsByUserName().subscribe({
      next: users => this.friendsListUsers = users,
      error: err => console.error(err)
    });
  }

  removeFriend(friendName: string) {
    this.membersService.removeFriend(friendName).subscribe({
      next: () => {
        this.toastr.success('Friend removed');
        this.loadfriendListUsers();
      },
      error: err => this.toastr.error(err)
    });
  }
}
