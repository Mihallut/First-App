import { Component, Input, OnInit } from '@angular/core';
import { Card } from 'src/app/shared/models/card.model';
import { DatePipe } from '@angular/common';
import { TaskboardService } from 'src/app/shared/taskboard.service';
import { MatDialog } from '@angular/material/dialog';
import { CardModalComponent } from '../card-modal/card-modal.component';
import { EditCardModalComponent } from '../edit-card-modal/edit-card-modal.component';
import { FormBuilder } from '@angular/forms';
import { Guid } from 'guid-typescript';
import { MatSnackBarHorizontalPosition, MatSnackBarVerticalPosition, MatSnackBar } from '@angular/material/snack-bar';
import { DeleteDialogModalComponent } from '../delete-dialog-modal/delete-dialog-modal.component';

@Component({
  selector: 'app-card',
  templateUrl: './card.component.html',
  styleUrls: ['./card.component.css'],
  providers: [DatePipe]
})
export class CardComponent {
  @Input() card!: Card;

  horizontalPosition: MatSnackBarHorizontalPosition = 'center';
  verticalPosition: MatSnackBarVerticalPosition = 'top';

  constructor(private datePipe: DatePipe,
    public service: TaskboardService,
    public dialog: MatDialog,
    private formBuilder: FormBuilder,
    private _snackBar: MatSnackBar) { }

  transformDate() {
    return this.datePipe.transform(this.card.dueDate, 'EEE, dd MMM');
  }

  getPriorityColor(priority: string) {
    switch (priority) {
      case 'High':
        return '#ff6b6b';
      case 'Medium':
        return '#ffff99';
      case 'Low':
        return '#ccff66';
      default:
        return 'none';
    }
  }

  getLaskLists() {
    var taskLists = this.service.taskLists;
    let filteredTaskLists = taskLists.filter(taskList => taskList.id !== this.card.taskListId);
    return filteredTaskLists;
  }

  openCardModal() {
    let dialogRef = this.dialog.open(CardModalComponent, {
      data: { card: this.card }
    });

    dialogRef.afterClosed().subscribe(result => {
      this.service.curentOpenedModalCard = this.service.initializeEmptyCard();
    });
  }

  openEditCardModal() {
    this.dialog.open(EditCardModalComponent, {
      data: { card: this.card }
    });
  }

  onSelectionChange(selectedValue: any) {
    let editCardForm = this.formBuilder.group({
      Title: [this.card.title],
      TaskListId: [selectedValue],
      DueDate: [this.card.dueDate],
      PriorityId: [this.card.priority.id],
      Description: [this.card.description]
    })

    this.service.editCard(this.card.id as Guid, editCardForm).subscribe({
      next: res => {
        this.service.refreshList();
        this._snackBar.open('Card successfuly moved', 'Ok', {
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
        console.log(err)
      }
    })
  }

  deleteCardClick() {
    let dialogRef = this.dialog.open(DeleteDialogModalComponent, {
      data: { componentName: this.card.title, componentId: this.card.id, componentType: 'card' }
    });
  }
}
