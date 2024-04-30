import { Component, OnInit, } from '@angular/core';
import { TaskboardService } from './shared/taskboard.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  template: `
    <mat-toolbar #toolbar class="fixed-toolbar">
    </mat-toolbar>
    <div #placeholder></div>
  `,
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  opened = false

  constructor(public service: TaskboardService) { }

  ngOnInit(): void {
    this.service.getHistoryPaged(1);
  }

  ShowMoreClick(): void {
    if (this.service.historyPaged.items.length == this.service.historyPaged.totalItems) {
      return;
    } else {
      let nextPage = Math.floor(this.service.historyPaged.items.length / 20) + 1;
      this.service.getHistoryPaged(nextPage);
    }
  }

  title = 'first-app.client';
}
