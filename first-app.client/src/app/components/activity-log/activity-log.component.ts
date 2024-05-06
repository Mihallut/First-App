import { Component, Input } from '@angular/core';
import { ActivityLog } from 'src/app/shared/models/activity-log.model';
import { DatePipe } from '@angular/common';


@Component({
  selector: 'app-activity-log',
  templateUrl: './activity-log.component.html',
  styleUrls: ['./activity-log.component.css'],
  providers: [DatePipe]
})
export class ActivityLogComponent {
  @Input() activityLog!: ActivityLog;

  constructor(private datePipe: DatePipe) { }

  transformDate() {
    return this.datePipe.transform(this.activityLog.creationDate, 'MMM d, h:mm a');
  }

  getMessage() {
    switch (this.activityLog.activityLogType.name) {
      case 'Create':
        return 'You added <strong>' + this.activityLog.changedCardTitle
          + '</strong> to <u>' + this.activityLog.valueAfter
          + '</u>';
      case 'Move':
        return 'You moved <strong>' + this.activityLog.changedCardTitle
          + '</strong> from <u>' + this.activityLog.valueBefore + '</u> to <u>'
          + this.activityLog.valueAfter + '</u>';
      case 'Rename':
        return 'You renamed <strong>' + this.activityLog.valueBefore
          + '</strong> to <strong>' + this.activityLog.valueAfter
          + '</strong> ';
      case 'Edit':
        if (this.activityLog.changedFieldName == 'Priority') {
          return 'You changed the priority of <strong>' + this.activityLog.changedCardTitle
            + '</strong> from <i>' + this.activityLog.valueBefore +
            '</i> to <i>' + this.activityLog.valueAfter +
            '</i>';
        } else if (this.activityLog.changedFieldName == 'Description') {
          return 'You edited the description of <strong>' + this.activityLog.changedCardTitle + '</strong>';
        } else if (this.activityLog.changedFieldName == 'DueDate') {
          return 'You edited the due date of <strong>' + this.activityLog.changedCardTitle + '</strong> from '
            + this.activityLog.valueBefore + ' to ' + this.activityLog.valueAfter;
        } else {
          return 'Unknown action';
        }
      case 'Delete':
        return 'You deleted <strong>' + this.activityLog.changedCardTitle + '</strong> from <u>' + this.activityLog.valueBefore + '</u>';
      default:
        return 'Unknown action';
    }
  }

}
