import { inject } from '@angular/core';
import {ActivatedRouteSnapshot, Resolve, ResolveFn } from '@angular/router';
import { Observable } from 'rxjs';
import { MembersService } from 'src/app/members/members.service';
import { Member } from 'src/app/shared/models/member';

export const MemberDetailedResolver: ResolveFn<Member> = (route, state) => {
  const memberService = inject(MembersService);

  return memberService.getMember(route.paramMap.get('username')!)
};

