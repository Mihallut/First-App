import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { DatePipe } from '@angular/common';
import { TaskboardService } from 'src/app/shared/taskboard.service';
import { FormBuilder } from '@angular/forms';
import { Guid } from 'guid-typescript';

@Component({
  selector: 'app-edit-card-modal',
  templateUrl: './edit-card-modal.component.html',
  styleUrls: ['./edit-card-modal.component.css'],
  providers: [DatePipe]
})
export class EditCardModalComponent implements OnInit {
  minDate: Date;
  highPriorityId: number;
  mediumPriorityId: number;
  lowPriorityId: number;

  constructor(
    public dialogRef: MatDialogRef<EditCardModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    public service: TaskboardService,
    private datePipe: DatePipe,
    private formBuilder: FormBuilder) {

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

  ngOnInit(): void {
    this.service.getCardHistoryPaged(1, this.data.card.id);
  }

  transformDate(date: Date) {
    return this.datePipe.transform(date, 'yyyy-MM-dd');
  }

  acceptEditCardModal() {
    let dueDate = this.editCardForm.get('DueDate')?.value as Date;
    let formatedDate = this.transformDate(dueDate)
    this.editCardForm.get('DueDate')?.setValue(formatedDate);

    this.service.editCard(this.data.card.id as Guid, this.editCardForm)
    this.dialogRef.close();
  }
}

