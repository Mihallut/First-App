import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { DatePipe } from '@angular/common';
import { TaskboardService } from 'src/app/shared/taskboard.service';
import { FormBuilder } from '@angular/forms';
import { Guid } from 'guid-typescript';
import { Card } from 'src/app/shared/models/card.model';
import { MatSnackBarHorizontalPosition, MatSnackBarVerticalPosition, MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-edit-card-modal',
  templateUrl: './edit-card-modal.component.html',
  styleUrls: ['./edit-card-modal.component.css'],
  providers: [DatePipe]
})
export class EditCardModalComponent {
  minDate: Date;
  highPriorityId: number;
  mediumPriorityId: number;
  lowPriorityId: number;
  horizontalPosition: MatSnackBarHorizontalPosition = 'center';
  verticalPosition: MatSnackBarVerticalPosition = 'top';

  constructor(
    public dialogRef: MatDialogRef<EditCardModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    public service: TaskboardService,
    private datePipe: DatePipe,
    private formBuilder: FormBuilder,
    private _snackBar: MatSnackBar) {

    let currentDate = new Date();
    this.minDate = new Date(currentDate.setFullYear(currentDate.getFullYear() - 1));

    this.highPriorityId = 1;
    this.mediumPriorityId = 2;
    this.lowPriorityId = 3;
  }

  editCardForm = this.formBuilder.group({
    Title: [this.data.card.title],
    TaskListId: [this.data.card.taskListId],
    DueDate: [this.data.card.dueDate],
    PriorityId: [this.data.card.priority.id],
    Description: [this.data.card.description]
  })

  transformDate(date: Date) {
    return this.datePipe.transform(date, 'yyyy-MM-dd');
  }

  saveEditCardModal() {
    let dueDate = this.editCardForm.get('DueDate')?.value as Date;
    let formatedDate = this.transformDate(dueDate)
    this.editCardForm.get('DueDate')?.setValue(formatedDate);

    this.service.editCard(this.data.card.id as Guid, this.editCardForm).subscribe({
      next: res => {
        if (this.service.curentOpenedModalCard.title != '') {
          this.service.curentOpenedModalCard = res as Card
          this.service.getCardHistoryPaged(1, this.service.curentOpenedModalCard.id);
        }
        this.service.refreshList();
        this.service.getHistoryPaged(1)
        this.dialogRef.close();
        this._snackBar.open('Card successfuly edited', 'Ok', {
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

        if (err.status == 400) {
          // Bad Request response
          this.service.setFluentValidationErrors(this.editCardForm, err.error.errors);
        }
        console.log(err)
      }
    })
  }
}

