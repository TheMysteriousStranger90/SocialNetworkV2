import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-dialog',
  template: `
    <img [src]="data.image">
  `,
})
export class DialogComponent {
  constructor(@Inject(MAT_DIALOG_DATA) public data: {image: string}) {}
}
