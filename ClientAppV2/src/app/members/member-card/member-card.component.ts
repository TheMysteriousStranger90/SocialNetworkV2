import { Component, Input, OnInit } from '@angular/core';
import { Member } from 'src/app/shared/models/member';
import { MembersService } from '../members.service';
import { ToastrService } from 'ngx-toastr';
import { PresenceService } from 'src/app/core/services/presence.service';

@Component({
  selector: 'app-member-card',
  templateUrl: './member-card.component.html',
  styleUrls: ['./member-card.component.scss']
})
export class MemberCardComponent implements OnInit {
  @Input() member: Member | undefined;

  constructor(private memberService: MembersService, private toastr: ToastrService,
              public presenceService: PresenceService) {
  }

  ngOnInit(): void {
  }

  addLike(member: Member) {
    if (member && member.userName) {
      this.memberService.addLike(member.userName).subscribe({
        next: () => this.toastr.success('You have liked ' + member.userName)
      })
    } else {
      console.error('Username is not defined');
    }
  }

  addFollow(member: Member) {
    if (member && member.userName) {
      this.memberService.followUser(member.userName).subscribe({
        next: () => this.toastr.success('You have followed ' + member.userName)
      })
    } else {
      console.error('Username is not defined');
    }
  }

  sendFriendRequest(member: Member) {
    if (member && member.userName) {
      this.memberService.sendFriendRequest(member.userName).subscribe({
        next: () => this.toastr.success('Friend request sent to ' + member.userName),
        error: error => this.toastr.error((error.error && error.error.message) || error.statusText)
      })
    } else {
      console.error('Username is not defined');
    }
  }
}
