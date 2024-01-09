import { Component, Input, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MatTabGroup } from '@angular/material/tabs';
import { take } from 'rxjs';
import { Member } from 'src/app/shared/models/member';
import { Message } from 'src/app/shared/models/message';
import { User } from 'src/app/shared/models/user';
import { PresenceService } from 'src/app/core/services/presence.service';
import { MessagesService } from 'src/app/messages/messages.service';
import { AccountService } from 'src/app/account/account.service';
import { GalleryItem, GalleryModule, ImageItem } from 'ng-gallery';
import { Rating } from 'src/app/shared/models/rating';

@Component({
  selector: 'app-member-detail',
  templateUrl: './member-detail.component.html',
  styleUrls: ['./member-detail.component.scss']
})
export class MemberDetailComponent implements OnInit, OnDestroy {
  @ViewChild('memberTabs', { static: true }) memberTabs?: MatTabGroup;
  member: Member = {} as Member;
  messages: Message[] = [];
  user?: User;
  images: GalleryItem[] = [];

  @Input() photoId: number | undefined;

  averageRating: number | undefined;

  constructor(public presenceService: PresenceService, private route: ActivatedRoute,
              private messageService: MessagesService, private accountService: AccountService) {
    this.accountService.currentUser$.pipe(take(1)).subscribe({
      next: user => {
        if (user) this.user = user;
      }
    })
  }

  ngOnInit(): void {
    this.route.data.subscribe({
      next: data => {
        this.member = data['member'];
        this.getImages();
      }
    })

    this.route.queryParams.subscribe({
      next: params => {
        params['tab'] && this.selectTab(params['tab'])
      }
    })
  }

  ngOnDestroy(): void {
    this.messageService.stopHubConnection();
  }

  loadMessages() {
    if (this.member)
      this.messageService.getMessageThread(this.member.userName).subscribe({
        next: messages => this.messages = messages
      })
  }

  selectTab(tabId: number) {
    if (this.memberTabs) {
      this.memberTabs.selectedIndex = tabId;
    }
  }

  onTabActivated(event: any) {
    if (event.index === 3 && this.user) {
      this.messageService.createHubConnection(this.user, this.member.userName);
    } else {
      this.messageService.stopHubConnection();
    }
  }

  getImages() {
    if (!this.member) return;
    for (const photo of this.member?.photos) {
      this.photoId = photo.id;
      this.images.push(new ImageItem({ src: photo.url }));
    }
  }
}
