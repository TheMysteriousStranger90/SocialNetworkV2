import { Component } from '@angular/core';
import { Message } from '../shared/models/message';
import { Pagination } from '../shared/models/pagination';
import { MessagesService } from './messages.service';
import { PageEvent } from '@angular/material/paginator';

@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html',
  styleUrls: ['./messages.component.scss']
})
export class MessagesComponent {

  messages?: Message[];
  pagination: Pagination = {
    currentPage: 1,
    itemsPerPage: 8,
    totalItems: 0,
    totalPages: 0
  };
  container = 'Unread';
  pageNumber = 1;
  pageSize = 8;
  totalCount = 0;
  loading = false;
  displayedColumns: string[] = ['message', 'fromTo', 'sentReceived', 'delete'];


  constructor(private messagesService: MessagesService) {
  }

  ngOnInit(): void {
    this.loadMessages();
  }

  loadMessages() {
    this.loading = true;
    this.messagesService.getMessages(this.pagination.currentPage, this.pagination.itemsPerPage, this.container)
      .subscribe(response => {
        this.messages = response.result;
        if (response.pagination) {
          this.pagination = response.pagination;
        }
        this.loading = false;
      }, error => {
        this.loading = false;
      });
  }

  deleteMessage(id: number) {
    this.messagesService.deleteMessage(id).subscribe({
      next: () => this.messages?.splice(this.messages.findIndex(m => m.id === id), 1)
    })
  }

  pageChanged(event: PageEvent) {
    this.pagination.currentPage = event.pageIndex + 1;
    this.pagination.itemsPerPage = event.pageSize;
    this.loadMessages();
  }
}

