import { Component, Input } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar, MatSnackBarHorizontalPosition, MatSnackBarVerticalPosition } from '@angular/material/snack-bar';
import { Board } from 'src/app/shared/models/board.model';
import { TaskboardService } from 'src/app/shared/taskboard.service';
import { DeleteDialogModalComponent } from '../delete-dialog-modal/delete-dialog-modal.component';
import { AppComponent } from 'src/app/app.component';

@Component({
  selector: 'app-board-list',
  templateUrl: './board-list.component.html',
  styleUrls: ['./board-list.component.css']
})
export class BoardListComponent {
  @Input() board!: Board;
  @Input() parent!: AppComponent;

  isEditingBoard = false;
  horizontalPosition: MatSnackBarHorizontalPosition = 'center';
  verticalPosition: MatSnackBarVerticalPosition = 'top';

  editBoardForm = this.formBuilder.group({
    NewName: ['']
  })

  constructor(public dialog: MatDialog, private formBuilder: FormBuilder, private _snackBar: MatSnackBar,
    public service: TaskboardService) {

  }

  toggleEditBoard() {
    this.isEditingBoard = !this.isEditingBoard;
    if (this.isEditingBoard) {
      this.editBoardForm.get('NewName')?.setValue(this.board.name);
    } else {
      this.editBoardForm.reset();
    }
  }

  deleteBoardClick() {
    let dialogRef = this.dialog.open(DeleteDialogModalComponent, {
      data: { componentName: this.board.name, componentId: this.board.id, componentType: 'board' }
    });
  }

  saveEditBoard() {
    if (this.editBoardForm.get('NewName')?.value != this.board.name) {
      this.service.editBoard(this.board.id, this.editBoardForm)
        .subscribe({
          next: res => {
            this.service.getBoards();
            this.isEditingBoard = !this.isEditingBoard;
            this.editBoardForm.reset();
            this._snackBar.open('Board renamed', 'Ok', {
              horizontalPosition: this.horizontalPosition,
              verticalPosition: this.verticalPosition,
              panelClass: ['success-snackbar'],
              duration: 3000
            });
          },
          error: err => {
            this._snackBar.open('Server respond with status code ' + err.status + ". Make sure you fill in all fields ", 'Ok', {
              horizontalPosition: this.horizontalPosition,
              verticalPosition: this.verticalPosition,
              panelClass: ['error-snackbar'],
              duration: 10000
            });

            if (err.status == 400) {
              // Bad Request response
              this.service.setFluentValidationErrors(this.editBoardForm, err.error.errors);
            }
            console.log(err)
          }
        })
    } else {
      const control = this.editBoardForm.get('NewName');
      control?.setErrors({
        fluentValidationError: ('You dont change name')
      });
    }
  }

  onBoardClick() {
    this.service.curentSelectedBoard = this.board
    this.service.refreshList()
    this.service.updateHistoryPaged()
    this.parent.openedBoards = !this.parent.openedBoards
  }
}
