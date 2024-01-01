import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HasRoleDirective } from './directives/has-role.directive';
import { SharedModule } from '../shared/shared.module';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { ToastrModule } from 'ngx-toastr';
import { RouterModule } from '@angular/router';
import { RolesModalComponent } from './modals/roles-modal/roles-modal.component';

import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatDialogModule } from '@angular/material/dialog';
import { FormsModule } from '@angular/forms';
import { NotificationComponent } from './notification/notification.component';

@NgModule({
  declarations: [
    HasRoleDirective,
    NavBarComponent,
    RolesModalComponent,
    NotificationComponent
  ],
  imports: [
    ToastrModule.forRoot(
      { positionClass: 'toast-bottom-right',
        preventDuplicates: true}

    ),
    SharedModule,
    CommonModule,
    RouterModule,




    MatCheckboxModule,
    MatFormFieldModule,
    MatDialogModule,
    FormsModule
  ],
  exports: [
    NavBarComponent,
    HasRoleDirective,
    RolesModalComponent,



    

  ]
})
export class CoreModule { }
