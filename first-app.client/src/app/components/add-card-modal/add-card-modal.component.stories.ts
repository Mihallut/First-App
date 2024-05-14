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
import { AutosizeModule } from 'ngx-autosize';

import { Component, Input } from '@angular/core';
import { AddCardModalComponent } from './add-card-modal.component';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';

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
        this._dialog.open(AddCardModalComponent, {
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
    title: 'AddCardModalComponent',
    component: AddCardModalComponent,
    decorators: [
        moduleMetadata({
            declarations: [AddCardModalComponent],
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
                MatSelectModule,
                AutosizeModule,
                MatFormFieldModule,
                MatInputModule
            ],
            providers: [
                {
                    provide: TaskboardService,
                    useValue: {
                        taskLists: [
                            { id: '3fa85f64-5717-4562-b3fc-2c963f66afa6', name: 'TaskList 1' },
                            { id: '3fa85f64-5717-4562-b3fc-2c963f66afa7', name: 'TaskList 2' },
                            { id: '3fa85f64-5717-4562-b3fc-2c963f66afa8', name: 'TaskList 3' }
                        ]
                    }
                },
                {
                    provide: MatDialogRef,
                    useValue: {}
                },
                {
                    provide: MAT_DIALOG_DATA,
                    useValue: {
                        taskList: {
                            id: "3fa85f64-5717-4562-b3fc-2c963f66afa6",
                            title: "TaskList 1"
                        }
                    }
                }
            ]
        }),
    ],
};

export const Default = () => ({
    component: AddCardModalComponent,
    props: {
    },
});
