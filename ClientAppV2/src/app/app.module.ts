import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { SharedModule } from './shared/shared.module';
import {HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { CoreModule } from './core/core.module';
import { JwtInterceptor } from './core/interceptors/jwt.interceptor';
import { LikesComponent } from './likes/likes.component';
import { FormsModule } from '@angular/forms';
import { FollowComponent } from './follow/follow.component';
import { PhotoEditorComponent } from './members/photo-editor/photo-editor.component';
import { FileUploadModule } from 'ng2-file-upload';
import { LoadingInterceptor } from './core/interceptors/loading.interceptor';
import { MessagesComponent } from './messages/messages.component';
import { TimeagoModule } from 'ngx-timeago';
import { HomeComponent } from './home/home.component';
import { MemberCardComponent } from './members/member-card/member-card.component';
import { MemberListComponent } from './members/member-list/member-list.component';
import { MemberMessagesComponent } from './members/member-messages/member-messages.component';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { GalleryModule } from 'ng-gallery';

@NgModule({
  declarations: [
    AppComponent,
    LikesComponent,
    FollowComponent,
    PhotoEditorComponent,
    MessagesComponent,
    HomeComponent,
    MemberCardComponent,
    MemberListComponent,
    MemberMessagesComponent,
    MemberDetailComponent
  ],
  imports: [
    GalleryModule,
    HttpClientModule,
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    SharedModule,
    CoreModule,
    FormsModule,
    FileUploadModule,
    TimeagoModule.forRoot()
  ],
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true},
    {provide: HTTP_INTERCEPTORS, useClass: LoadingInterceptor, multi: true},
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
