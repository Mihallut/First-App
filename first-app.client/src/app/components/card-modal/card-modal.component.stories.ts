import { CardModalComponent } from '../card-modal/card-modal.component';
import { moduleMetadata } from '@storybook/angular';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { TaskboardService } from 'src/app/shared/taskboard.service';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatMenuModule } from '@angular/material/menu';
import { MatCardModule } from '@angular/material/card';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatSelectModule } from '@angular/material/select';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ActivityLogComponent } from '../activity-log/activity-log.component';


import { of } from 'rxjs';
import { Guid } from 'guid-typescript';
import { Component, Input } from '@angular/core';

@Component({
    template: `
      <button mat-raised-button color="primary" (click)="launch()"> Launch </button>
    `
})

class LaunchDialogComponent {
    @Input() title = '';
    @Input() description = '';
    @Input() width = '';
    constructor(private _dialog: MatDialog) { }

    launch(): void {
        this._dialog.open(CardModalComponent, {
            autoFocus: false,
            width: this.width,
            data: {
                title: this.title,
                description: this.description
            }
        });
    }
}

export default {
    title: 'CardModalComponent',
    component: CardModalComponent,
    decorators: [
        moduleMetadata({
            declarations: [CardModalComponent,
                ActivityLogComponent],
            imports: [
                CommonModule,
                FormsModule,
                ReactiveFormsModule,
                BrowserAnimationsModule,
                MatDialogModule,
                MatSnackBarModule,
                MatMenuModule,
                MatCardModule,
                MatToolbarModule,
                MatButtonModule,
                MatIconModule,
                MatSelectModule
            ],
            providers: [
                {
                    provide: TaskboardService,
                    useValue: {
                        taskLists: [
                            { id: '3fa85f64-5717-4562-b3fc-2c963f66afa6', name: 'TaskList 1' },
                            { id: '3fa85f64-5717-4562-b3fc-2c963f66afa7', name: 'TaskList 2' },
                            { id: '3fa85f64-5717-4562-b3fc-2c963f66afa8', name: 'TaskList 3' },
                        ],
                        activityLogsForCard: {
                            items: [
                                {
                                    changedCardTitle: "Card title",
                                    creationDate: new Date(),
                                    valueBefore: '',
                                    valueAfter: 'TaskList 1',
                                    changedFieldName: "TaskList",
                                    activityLogType: {
                                        id: 1,
                                        name: "Create"
                                    }
                                }
                            ],
                            totalItems: 1
                        }
                    }
                },
                {
                    provide: MatDialogRef,
                    useValue: {}
                },
                {
                    provide: MAT_DIALOG_DATA,
                    useValue: {
                        card: {
                            id: Guid.create(),
                            title: "Card title",
                            description: "Card description",
                            dueDate: new Date(),
                            taskListId: "3fa85f64-5717-4562-b3fc-2c963f66afa6",
                            priority: {
                                id: 1,
                                name: "High"
                            }
                        }
                    }
                }
            ]
        }),
    ],
};

export const Default = () => ({
    component: CardModalComponent,
    props: {
    },
});
