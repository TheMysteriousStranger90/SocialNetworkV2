import { Component, OnInit } from '@angular/core';
import { MembersService } from '../members/members.service';

@Component({
  selector: 'app-follow',
  templateUrl: './follow.component.html',
  styleUrls: ['./follow.component.scss']
})
export class FollowComponent implements OnInit {
  followedUsers: any;

  constructor(private membersService: MembersService) { }

  ngOnInit(): void {
    this.loadFollowed();
  }

  loadFollowed() {
    this.membersService.getFollowed().subscribe({
      next: users => this.followedUsers = users,
      error: err => console.error(err)
    });
  }
}
