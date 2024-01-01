import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CoreModule } from '../core/core.module';
import { SharedModule } from '../shared/shared.module';
import { AdminRoutingModule } from './admin-routing.module';
import { LockUnlockComponent } from './lock-unlock/lock-unlock.component';
import { AdminPanelComponent } from './admin-panel/admin-panel.component';
import { PhotoManagementComponent } from './photo-management/photo-management.component';
import { FlexLayoutModule } from '@angular/flex-layout';
import { UserManagementComponent } from './user-management/user-management.component';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    LockUnlockComponent,
    AdminPanelComponent,
    PhotoManagementComponent,
    UserManagementComponent
  ],
  imports: [
    CommonModule,
    CoreModule,
    SharedModule,
    AdminRoutingModule,
    FlexLayoutModule,
    FormsModule
  ]
})
export class AdminModule {
}

