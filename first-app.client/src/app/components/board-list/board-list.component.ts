import { Component, Input } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar, MatSnackBarHorizontalPosition, MatSnackBarVerticalPosition } from '@angular/material/snack-bar';
import { Board } from 'src/app/shared/models/board/board.model';
import { TaskboardService } from 'src/app/shared/taskboard.service';
import { DeleteDialogModalComponent } from '../delete-dialog-modal/delete-dialog-modal.component';
import { AppComponent } from 'src/app/app.component';
import { Observable } from 'rxjs';
import { Store, select } from '@ngrx/store';
import { AppState } from 'src/app.state';
import * as BoardsAction from 'src/app/store/boards/boards.actions'
import * as selectors from 'src/app/store/boards/boards.selectors';


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
    Name: ['']
  })

  constructor(public dialog: MatDialog, private formBuilder: FormBuilder, private _snackBar: MatSnackBar,
    public service: TaskboardService,
    private store: Store<AppState>) {
    this.store.pipe(select(selectors.isEdittingSuccessSelector)).subscribe(isSuccess => {
      if (isSuccess) {
        this.editBoardForm.reset();
      }
    });
    this.store.select(selectors.errorsSelector).subscribe(err => {
      if (err) {
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
    });
  }

  toggleEditBoard() {
    this.isEditingBoard = !this.isEditingBoard;
    if (this.isEditingBoard) {
      this.editBoardForm.get('Name')?.setValue(this.board.name);
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
    this.store.dispatch(BoardsAction.editBoard({ boardId: this.board.id, name: { name: this.editBoardForm.get('Name')?.value?.toString() } }));
  }

  onBoardClick() {
    this.service.curentSelectedBoard = this.board
    this.service.refreshList()
    this.service.updateHistoryPaged()
    this.parent.openedBoards = !this.parent.openedBoards
  }
}
