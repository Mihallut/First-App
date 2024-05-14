import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { DatePipe } from '@angular/common';
import { TaskboardService } from 'src/app/shared/taskboard.service';
import { MatDialog } from '@angular/material/dialog';
import { EditCardModalComponent } from '../edit-card-modal/edit-card-modal.component';
import { Card } from 'src/app/shared/models/card.model';

@Component({
  selector: 'app-card-modal',
  templateUrl: './card-modal.component.html',
  styleUrls: ['./card-modal.component.css'],
  providers: [DatePipe]
})
export class CardModalComponent implements OnInit {
  card: Card;
  constructor(
    public dialogRef: MatDialogRef<CardModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    public service: TaskboardService,
    private datePipe: DatePipe,
    public dialog: MatDialog) {

    this.card = this.data.card as Card;
  }

  ngOnInit(): void {
    this.service.getCardHistoryPaged(1, this.data.card.id);
  }

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

  getLaskListName() {
    var taskLists = this.service.taskLists;
    let cardTaskList = taskLists.find(taskList => taskList.id == this.card.taskListId);
    return cardTaskList?.name;
  }

  openEditCardModal() {
    this.dialog.open(EditCardModalComponent, {
      data: { card: this.card }
    });
  }
}
