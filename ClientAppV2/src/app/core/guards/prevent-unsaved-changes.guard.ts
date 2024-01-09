import { inject } from '@angular/core';
import { CanDeactivateFn } from '@angular/router';
import { MemberEditComponent } from 'src/app/members/member-edit/member-edit.component';
import { ConfirmService } from '../services/confirm.service';

export const PreventUnsavedChangesGuard: CanDeactivateFn<MemberEditComponent> = (component) => {
  const confirmService = inject(ConfirmService);

  if (component.editForm?.dirty) {
    return confirmService.confirm();
  }

  return true;
};
