import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { DatePipe } from '@angular/common';
import { TaskboardService } from 'src/app/shared/taskboard.service';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatSnackBar, MatSnackBarHorizontalPosition, MatSnackBarVerticalPosition } from '@angular/material/snack-bar';


@Component({
  selector: 'app-add-card-modal',
  templateUrl: './add-card-modal.component.html',
  styleUrls: ['./add-card-modal.component.css'],
  providers: [DatePipe]
})
export class AddCardModalComponent {
  minDate: Date;
  highPriorityId: number;
  mediumPriorityId: number;
  lowPriorityId: number;
  currentDate: Date;
  horizontalPosition: MatSnackBarHorizontalPosition = 'center';
  verticalPosition: MatSnackBarVerticalPosition = 'top';

  constructor(
    public dialogRef: MatDialogRef<AddCardModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    public service: TaskboardService,
    private datePipe: DatePipe,
    private formBuilder: FormBuilder,
    private _snackBar: MatSnackBar) {

    this.currentDate = new Date();
    this.minDate = new Date(this.currentDate.setFullYear(this.currentDate.getFullYear() - 1));

    this.highPriorityId = 1;
    this.mediumPriorityId = 2;
    this.lowPriorityId = 3;
  }

  addCardForm = this.formBuilder.group({
    Title: [''],
    TaskListId: [this.data.taskList.id],
    DueDate: [this.transformDate(new Date()) as any],
    PriorityId: [''],
    Description: ['']
  })

  transformDate(date: Date) {
    return this.datePipe.transform(date, 'yyyy-MM-dd');
  }

  saveAddCardModal() {
    let dueDate = this.addCardForm.get('DueDate')?.value as Date;
    let formatedDate = this.transformDate(dueDate)
    this.addCardForm.get('DueDate')?.setValue(formatedDate);

    this.service.addCard(this.addCardForm)
      .subscribe({
        next: res => {
          this.service.refreshList();
          this.service.getHistoryPaged(1)
          this.dialogRef.close();
          this._snackBar.open('New card added to ' + this.data.taskList.name, 'Ok', {
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
            this.service.setFluentValidationErrors(this.addCardForm, err.error.errors);
          }
          console.log(err)
        }
      })
  }

}
