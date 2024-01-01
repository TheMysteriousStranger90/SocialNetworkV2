import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { LockUnlockComponent } from './lock-unlock/lock-unlock.component';
import { AdminPanelComponent } from './admin-panel/admin-panel.component';
import { PhotoManagementComponent } from './photo-management/photo-management.component';
import { UserManagementComponent } from './user-management/user-management.component';

const routes: Routes = [
  {path: 'admin-panel', component: AdminPanelComponent},
  {path: 'user-management', component: UserManagementComponent},
  {path: 'photo-management', component: PhotoManagementComponent},
  {path: 'lock-unlock', component: LockUnlockComponent},
]

@NgModule({
  declarations: [

  ],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]
})
export class AdminRoutingModule { }

