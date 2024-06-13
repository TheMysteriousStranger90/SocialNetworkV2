import {Component, ElementRef, OnInit, ViewChild} from '@angular/core';
import {Member} from 'src/app/shared/models/member';
import {Pagination} from 'src/app/shared/models/pagination';
import {UserParams} from 'src/app/shared/models/userParams';
import {MembersService} from '../members.service';
import {PageEvent} from '@angular/material/paginator';
import {FormBuilder, FormGroup} from '@angular/forms';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.scss']
})
export class MemberListComponent implements OnInit {

  @ViewChild('search') searchTerm?: ElementRef;
  form: FormGroup;
  members: Member[] = [];
  userParams: UserParams | undefined;
  genderList = [{value: 'male', display: 'Males'}, {value: 'female', display: 'Females'}]
  pageNumber = 1;
  pageSize = 8;
  pagination: Pagination | undefined;

  constructor(private fb: FormBuilder, private memberService: MembersService) {
    this.form = this.fb.group({
      minAge: [''],
      maxAge: [''],
      gender: ['']
    });
    this.userParams = this.memberService.getUserParams();
  }

  ngOnInit(): void {
    this.loadMembers()
  }

  loadMembers() {
    if (this.userParams) {
      this.userParams.minAge = this.form.get('minAge')?.value || this.userParams.minAge;
      this.userParams.maxAge = this.form.get('maxAge')?.value || this.userParams.maxAge;
      this.userParams.gender = this.form.get('gender')?.value || this.userParams.gender;

      this.memberService.setUserParams(this.userParams);

      this.memberService.getMembers(this.userParams).subscribe({
        next: response => {
          if (response.result && response.pagination) {
            this.members = response.result;
            this.pagination = response.pagination;
          }
        },
        error: err => {
          console.error('Error getting members:', err.error.errors);
        }
      })
    }
  }

  onPageChanged(event: PageEvent) {
    if (this.userParams) {
      this.userParams.pageNumber = event.pageIndex + 1;
      this.userParams.pageSize = event.pageSize;
      this.memberService.setUserParams(this.userParams);
      this.memberService.getMembers(this.userParams).subscribe({
        next: response => {
          if (response.result && response.pagination) {
            this.members = response.result;
            this.pagination = response.pagination;
          }
        },
        error: err => {
          console.error('Error getting members:', err.error.errors);
        }
      });
    }
  }

  onReset() {
    if (this.searchTerm) this.searchTerm.nativeElement.value = '';
    this.userParams = this.memberService.resetUserParams();
    this.form.reset({
      minAge: '',
      maxAge: '',
      gender: ''
    });
    this.loadMembers();
  }

  onSearch() {
    const params = this.memberService.getUserParams();
    params!.search = this.searchTerm?.nativeElement.value;
    params!.pageNumber = 1;
    this.memberService.setUserParams(params!);
    this.userParams = params;
    this.loadMembers();
  }
}

