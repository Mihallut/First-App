import { Component, Input } from '@angular/core';
import { MatDialogRef, MatDialog, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { TaskList } from 'src/app/shared/models/taskList.model';
import { AddCardModalComponent } from '../add-card-modal/add-card-modal.component';
import { DeleteDialogModalComponent } from '../delete-dialog-modal/delete-dialog-modal.component';
import { FormBuilder } from '@angular/forms';
import { MatSnackBarHorizontalPosition, MatSnackBarVerticalPosition, MatSnackBar } from '@angular/material/snack-bar';
import { TaskboardService } from 'src/app/shared/taskboard.service';

@Component({
  selector: 'app-task-list',
  templateUrl: './task-list.component.html',
  styleUrls: ['./task-list.component.css']
})
export class TaskListComponent {
  @Input() tl!: TaskList;

  isEditingList: boolean = false;
  horizontalPosition: MatSnackBarHorizontalPosition = 'center';
  verticalPosition: MatSnackBarVerticalPosition = 'top';

  constructor(public dialog: MatDialog,
    private formBuilder: FormBuilder,
    private _snackBar: MatSnackBar,
    public service: TaskboardService) { }

  editTaskListForm = this.formBuilder.group({
    NewName: ['']
  })

  openAddCardModal() {
    this.dialog.open(AddCardModalComponent, {
      data: { taskList: this.tl }
    });
  }

  deleteTaskListClick() {
    let dialogRef = this.dialog.open(DeleteDialogModalComponent, {
      data: { componentName: this.tl.name, componentId: this.tl.id, componentType: 'taskList' }
    });
  }

  toggleAddList() {
    this.isEditingList = !this.isEditingList;
    if (this.isEditingList) {
      this.editTaskListForm.get('NewName')?.setValue(this.tl.name);
    } else {
      this.editTaskListForm.reset();
    }

  }

  saveEditTaskList() {
    if (this.editTaskListForm.get('NewName')?.value != this.tl.name) {
      this.service.editTaskList(this.tl.id, this.editTaskListForm)
        .subscribe({
          next: res => {
            this.service.refreshList();
            this.isEditingList = !this.isEditingList;
            this.editTaskListForm.reset();
            this._snackBar.open('Task list renamed', 'Ok', {
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
              this.service.setFluentValidationErrors(this.editTaskListForm, err.error.errors);
            }
            console.log(err)
          }
        })
    } else {
      const control = this.editTaskListForm.get('NewName');
      control?.setErrors({
        fluentValidationError: ('You dont change name')
      });
    }

  }
}
