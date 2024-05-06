import { Component, OnInit } from '@angular/core';
import { TaskboardService } from '../../shared/taskboard.service';
import { FormBuilder } from '@angular/forms';
import { MatSnackBarHorizontalPosition, MatSnackBarVerticalPosition, MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-taskboard',
  templateUrl: './taskboard.component.html',
  styleUrls: ['./taskboard.component.css']
})
export class TaskboardComponent implements OnInit {
  isAddingList: boolean = false;
  horizontalPosition: MatSnackBarHorizontalPosition = 'center';
  verticalPosition: MatSnackBarVerticalPosition = 'top';

  constructor(public service: TaskboardService,
    private formBuilder: FormBuilder,
    private _snackBar: MatSnackBar) {

  }

  addTaskListForm = this.formBuilder.group({
    Name: ['']
  })

  toggleAddList() {
    this.isAddingList = !this.isAddingList;
    this.addTaskListForm.reset();
  }

  ngOnInit(): void {
    this.service.refreshList();
  }

  saveAddTaskList() {
    this.service.addTaskList(this.addTaskListForm)
      .subscribe({
        next: res => {
          this.service.refreshList();
          this.isAddingList = !this.isAddingList;
          this.addTaskListForm.reset();
          this._snackBar.open('New task list added', 'Ok', {
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
            this.service.setFluentValidationErrors(this.addTaskListForm, err.error.errors);
          }
          console.log(err)
        }
      })
  }
}
