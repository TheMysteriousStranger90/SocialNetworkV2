import {Component, OnInit} from '@angular/core';
import {Member} from '../shared/models/member';
import {Pagination} from '../shared/models/pagination';
import {MembersService} from '../members/members.service';

@Component({
  selector: 'app-likes',
  templateUrl: './likes.component.html',
  styleUrls: ['./likes.component.scss']
})
export class LikesComponent implements OnInit {
  members: Member[] | undefined;
  predicate = 'liked';
  pageNumber = 1;
  pageSize = 8;
  pagination: Pagination | undefined;

  constructor(private memberService: MembersService) {
  }

  ngOnInit(): void {
    this.loadLikes();
  }

  loadLikes() {
    this.memberService.getLikes(this.predicate, this.pageNumber, this.pageSize).subscribe({
      next: response => {
        this.members = response.result;
        this.pagination = response.pagination;
      }
    })
  }

  pageChanged(event: any) {
    this.pageNumber = event;
    this.loadLikes();
  }
}
