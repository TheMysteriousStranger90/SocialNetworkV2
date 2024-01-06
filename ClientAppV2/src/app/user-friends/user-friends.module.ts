import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {FlexLayoutModule} from "@angular/flex-layout";
import {FormsModule} from "@angular/forms";
import { CoreModule } from '../core/core.module';
import { SharedModule } from '../shared/shared.module';
import { AdminRoutingModule } from '../admin/admin-routing.module';
import { UserFriendsRoutingModule } from './user-friends-routing.module';
import { FriendRequestComponent } from './friend-request/friend-request.component';
import { FriendListComponent } from './friend-list/friend-list.component';



@NgModule({
  declarations: [
    FriendRequestComponent,
    FriendListComponent
  ],
  imports: [
    CommonModule,
    CoreModule,
    SharedModule,
    UserFriendsRoutingModule,
    FlexLayoutModule,
    FormsModule
  ]
})
export class UserFriendsModule { }
