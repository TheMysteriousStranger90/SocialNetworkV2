import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {RouterModule, Routes} from "@angular/router";
import { FriendRequestComponent } from './friend-request/friend-request.component';
import { FriendListComponent } from './friend-list/friend-list.component';


const routes: Routes = [
  {path: 'friend-request', component: FriendRequestComponent},
  {path: 'friend-list', component: FriendListComponent}
]

@NgModule({
  declarations: [

  ],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]
})
export class UserFriendsRoutingModule { }
