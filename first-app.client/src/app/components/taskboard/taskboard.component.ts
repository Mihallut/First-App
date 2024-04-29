import { Component, OnInit } from '@angular/core';
import { TaskboardService } from '../../shared/taskboard.service';

@Component({
  selector: 'app-taskboard',
  templateUrl: './taskboard.component.html',
  styleUrls: ['./taskboard.component.css']
})
export class TaskboardComponent implements OnInit {
  /**
   *
   */
  constructor(public service: TaskboardService) {

  }

  ngOnInit(): void {
    this.service.refreshList();
  }
}
