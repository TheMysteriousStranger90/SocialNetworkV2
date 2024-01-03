import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { authGuard } from './core/guards/auth.guard';
import { adminGuard } from './core/guards/admin.guard';
import { LikesComponent } from './likes/likes.component';
import { NotificationComponent } from './core/notification/notification.component';
import { FollowComponent } from './follow/follow.component';
import { MessagesComponent } from './messages/messages.component';
import { HomeComponent } from './home/home.component';

const routes: Routes = [
  {path: '', component: HomeComponent, data: {breadcrumb: 'Home'}},
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [authGuard],
    children: [
      {path: 'likes', component: LikesComponent},
      {path: 'notification', component: NotificationComponent},
      {path: 'follow', component: FollowComponent},
      {path: 'messages', component: MessagesComponent},
      {
        path: 'admin', canActivate: [adminGuard],
        loadChildren: () => import('./admin/admin.module').then(m => m.AdminModule), data: {breadcrumb: {skip: true}}
      }
    ]
  },
  {
    path: 'account',
    loadChildren: () => import('./account/account.module').then(m => m.AccountModule),
    data: {breadcrumb: {skip: true}}
  },
  {path: '**', redirectTo: '', pathMatch: 'full'},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
