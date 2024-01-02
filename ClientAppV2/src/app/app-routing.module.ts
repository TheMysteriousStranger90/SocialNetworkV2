import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { authGuard } from './core/guards/auth.guard';
import { adminGuard } from './core/guards/admin.guard';
import { LikesComponent } from './likes/likes.component';
import { NotificationComponent } from './core/notification/notification.component';

const routes: Routes = [

  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [authGuard],
    children: [
      {path: 'likes', component: LikesComponent},

      {path: 'notification', component: NotificationComponent},
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
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
