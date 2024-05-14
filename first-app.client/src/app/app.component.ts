import { Component, OnInit, } from '@angular/core';
import { TaskboardService } from './shared/taskboard.service';
import { FormBuilder } from '@angular/forms';
import { MatSnackBarHorizontalPosition, MatSnackBarVerticalPosition, MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  template: `
    <mat-toolbar #toolbar class="fixed-toolbar">
    </mat-toolbar>
    <div #placeholder></div>
  `,
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
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
    private _snackBar: MatSnackBar) { }

  ngOnInit(): void {
    this.service.getBoards();
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
    this.service.addBoard(this.addBoardForm)
      .subscribe({
        next: res => {
          this.service.getBoards();
          this.isAddingBoard = !this.isAddingBoard;
          this.addBoardForm.reset();
          this._snackBar.open('New board added', 'Ok', {
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
            this.service.setFluentValidationErrors(this.addBoardForm, err.error.errors);
          }
          console.log(err)
        }
      })
  }
}
