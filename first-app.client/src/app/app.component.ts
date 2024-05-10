import { Component, OnInit, } from '@angular/core';
import { TaskboardService } from './shared/taskboard.service';
import { FormBuilder } from '@angular/forms';
import { MatSnackBarHorizontalPosition, MatSnackBarVerticalPosition, MatSnackBar } from '@angular/material/snack-bar';
import { Observable } from 'rxjs';
import { Board } from './shared/models/board/board.model';
import { Store, select } from '@ngrx/store';
import { AppState } from 'src/app.state';
import * as BoardsAction from './store/boards/boards.actions'
import * as selectors from './store/boards/boards.selectors';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent implements OnInit {
  Boards$: Observable<Board[]>;
  openedHistory = false
  openedBoards = false
  isAddingBoard = false
  horizontalPosition: MatSnackBarHorizontalPosition = 'center';
  verticalPosition: MatSnackBarVerticalPosition = 'top';

  addBoardForm = this.formBuilder.group({
    Name: ['']
  })

  constructor(public service: TaskboardService,
    private formBuilder: FormBuilder,
    private _snackBar: MatSnackBar,
    private store: Store<AppState>) {
    this.Boards$ = this.store.pipe(select(selectors.boardsSelector));
    this.store.pipe(select(selectors.isAddingSuccessSelector)).subscribe(isSuccess => {
      if (isSuccess) {
        this.isAddingBoard = !this.isAddingBoard;
        this.addBoardForm.reset();
        this._snackBar.open('New board added', 'Ok', {
          horizontalPosition: this.horizontalPosition,
          verticalPosition: this.verticalPosition,
          panelClass: ['success-snackbar'],
          duration: 3000
        });
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
          this.service.setFluentValidationErrors(this.addBoardForm, err.error.errors);
        }
        console.log(err)
      }
    });
  }

  ngOnInit(): void {
    this.store.dispatch(BoardsAction.getBoards());
  }

  ShowMoreClick(): void {
    if (this.service.historyPaged.items.length == this.service.historyPaged.totalItems) {
      return;
    } else {
      let nextPage = Math.floor(this.service.historyPaged.items.length / 20) + 1;
      this.service.getHistoryPaged(nextPage);
    }
  }

  toggleAddBoard() {
    this.isAddingBoard = !this.isAddingBoard;
    this.addBoardForm.reset();
  }

  saveAddBoard() {
    this.store.dispatch(BoardsAction.createBoard({ name: { name: this.addBoardForm.get('Name')?.value?.toString() } }));
  }
}
