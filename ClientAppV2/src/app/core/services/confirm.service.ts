import { Injectable } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { ConfirmDialogComponent } from '../modals/confirm-dialog/confirm-dialog.component';

@Injectable({
  providedIn: 'root'
})
export class ConfirmService {
  constructor(private dialog: MatDialog) { }

  confirm(
    title = 'Confirmation',
    message = 'Are you sure you want to do this?',
    btnOkText = 'Ok',
    btnCancelText = 'Cancel'
  ): Observable<boolean> {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      data: {
        title,
        message,
        btnOkText,
        btnCancelText
      }
    });

    return dialogRef.afterClosed().pipe(
      map(result => {
        return result;
      })
    );
  }
}