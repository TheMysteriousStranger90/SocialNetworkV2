import {AfterViewChecked, Component, ElementRef, Input, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MessagesService } from 'src/app/messages/messages.service';

@Component({
  selector: 'app-member-messages',
  templateUrl: './member-messages.component.html',
  styleUrls: ['./member-messages.component.scss']
})
export class MemberMessagesComponent implements OnInit, AfterViewChecked {
  @ViewChild('messageForm') messageForm?: NgForm
  @ViewChild('scrollMe') public scrollMe!: ElementRef;
  @Input() username?: string;
  messageContent = '';

  constructor(public messageService: MessagesService) { }

  ngOnInit(): void {
  }

  ngAfterViewChecked(): void {
    this.scrollToBottom();
  }

  sendMessage() {
    if (!this.username) return;
    this.messageService.sendMessage(this.username, this.messageContent).then(() => {
      this.messageForm?.reset();
    })
  }

  private scrollToBottom(): void {
    this.scrollMe.nativeElement.scrollTop = this.scrollMe.nativeElement.scrollHeight;
  }
}
