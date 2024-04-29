import { Component, Input, OnInit } from '@angular/core';
import { Card } from 'src/app/shared/models/card.model';
import { DatePipe } from '@angular/common';
import { TaskboardService } from 'src/app/shared/taskboard.service';
import { MatDialog } from '@angular/material/dialog';
import { CardModalComponent } from '../card-modal/card-modal.component';



@Component({
  selector: 'app-card',
  templateUrl: './card.component.html',
  styleUrls: ['./card.component.css'],
  providers: [DatePipe]
})
export class CardComponent {
  @Input() card!: Card;

  constructor(private datePipe: DatePipe, public service: TaskboardService, public dialog: MatDialog) { }

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

    this.dialog.open(CardModalComponent, {
      data: { card: this.card }
    });

  }
}
