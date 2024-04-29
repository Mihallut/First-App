import { Component, Input } from '@angular/core';
import { TaskList } from 'src/app/shared/models/taskList.model';

@Component({
  selector: 'app-task-list',
  templateUrl: './task-list.component.html',
  styleUrls: ['./task-list.component.css']
})
export class TaskListComponent {
  @Input() tl!: TaskList;
}
