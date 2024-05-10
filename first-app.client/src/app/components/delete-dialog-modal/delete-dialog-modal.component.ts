import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Component, Inject } from '@angular/core';
import { Guid } from 'guid-typescript';
import { MatSnackBarHorizontalPosition, MatSnackBarVerticalPosition, MatSnackBar } from '@angular/material/snack-bar';
import { DeleteDialogData } from 'src/app/shared/models/delete-dialog-data.model';
import { TaskboardService } from 'src/app/shared/taskboard.service';
import { Store, select } from '@ngrx/store';
import { AppState } from 'src/app.state';
import * as BoardsAction from 'src/app/store/boards/boards.actions'
import * as selectors from 'src/app/store/boards/boards.selectors';

@Component({
  selector: 'app-delete-dialog-modal',
  templateUrl: './delete-dialog-modal.component.html',
  styleUrls: ['./delete-dialog-modal.component.css']
})
export class DeleteDialogModalComponent {

  horizontalPosition: MatSnackBarHorizontalPosition = 'center';
  verticalPosition: MatSnackBarVerticalPosition = 'top';

  constructor(
    public dialogRef: MatDialogRef<DeleteDialogModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DeleteDialogData,
    private _snackBar: MatSnackBar,
    public service: TaskboardService,
    private store: Store<AppState>) { }

  onDeleteClick(): void {
    if (this.data.componentType == 'card') {
      this.deleteCardFunc();
    }
    else if (this.data.componentType == 'taskList') {
      this.deleteTaskListFunc()
    }
    else if (this.data.componentType == 'board') {
      this.deleteBoardFunc()
    }
    else {
      this._snackBar.open('Unknown command', 'Ok', {
        horizontalPosition: this.horizontalPosition,
        verticalPosition: this.verticalPosition,
        panelClass: ['error-snackbar'],
        duration: 10000
      });
    }

  }

  private deleteCardFunc() {
    this.service.deleteCard(this.data.componentId as Guid).subscribe({
      next: res => {
        this.service.refreshList();
        this.service.updateHistoryPaged()
        this.dialogRef.close();
        this._snackBar.open('Card successfuly deleted', 'Ok', {
          horizontalPosition: this.horizontalPosition,
          verticalPosition: this.verticalPosition,
          panelClass: ['success-snackbar'],
          duration: 3000
        });

      },
      error: err => {
        this._snackBar.open('Server respond with status code ' + err.status, 'Ok', {
          horizontalPosition: this.horizontalPosition,
          verticalPosition: this.verticalPosition,
          panelClass: ['error-snackbar'],
          duration: 10000
        });
        console.log(err);
      }
    });
  }

  private deleteTaskListFunc() {
    this.service.deleteTaskList(this.data.componentId as Guid).subscribe({
      next: res => {
        this.service.refreshList();
        this.service.updateHistoryPaged()
        this.dialogRef.close();
        this._snackBar.open('Task list successfuly deleted', 'Ok', {
          horizontalPosition: this.horizontalPosition,
          verticalPosition: this.verticalPosition,
          panelClass: ['success-snackbar'],
          duration: 3000
        });

      },
      error: err => {
        this._snackBar.open('Server respond with status code ' + err.status, 'Ok', {
          horizontalPosition: this.horizontalPosition,
          verticalPosition: this.verticalPosition,
          panelClass: ['error-snackbar'],
          duration: 10000
        });
        console.log(err);
      }
    });
  }

  private deleteBoardFunc() {
    this.store.dispatch(BoardsAction.deleteBoard({ boardId: this.data.componentId }));
    this.dialogRef.close();
  }
}
