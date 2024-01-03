import {ActivatedRouteSnapshot, Resolve, ResolveFn } from '@angular/router';
import { Observable } from 'rxjs';
import { MembersService } from 'src/app/members/members.service';
import { Member } from 'src/app/shared/models/member';

export class MemberDetailedResolver implements Resolve<Member> {
  constructor(private memberService: MembersService) {}

  resolve(route: ActivatedRouteSnapshot): Observable<Member> {
    return this.memberService.getMember(route.paramMap.get('username')!)
  }
}

