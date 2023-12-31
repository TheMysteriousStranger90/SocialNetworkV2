import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HasRoleDirective } from './directives/has-role.directive';
import { SharedModule } from '../shared/shared.module';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { ToastrModule } from 'ngx-toastr';



@NgModule({
  declarations: [
    HasRoleDirective,
    NavBarComponent
  ],
  imports: [
    ToastrModule.forRoot(
      { positionClass: 'toast-bottom-right',
        preventDuplicates: true}

    ),
    SharedModule,
    CommonModule
  ],
  exports: [
    NavBarComponent,
    HasRoleDirective,
  ]
})
export class CoreModule { }
