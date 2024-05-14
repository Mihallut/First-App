import { CardComponent } from './card.component';
import { moduleMetadata } from '@storybook/angular';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { TaskboardService } from 'src/app/shared/taskboard.service';
import { MatDialogModule } from '@angular/material/dialog';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatMenuModule } from '@angular/material/menu';
import { MatCardModule } from '@angular/material/card';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatSelectModule } from '@angular/material/select';
import { CardModalComponent } from '../card-modal/card-modal.component';


import { of } from 'rxjs';
import { Guid } from 'guid-typescript';

export default {
    title: 'CardComponent',
    component: CardComponent,
    decorators: [
        moduleMetadata({
            declarations: [CardComponent],
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
                        ]
                    }
                }
            ]
        }),
    ],
};

export const Default = () => ({
    component: CardComponent,
    props: {
        card: { id: Guid.create(), title: "Card title", description: "Card description", dueDate: new Date(), taskListId: "3fa85f64-5717-4562-b3fc-2c963f66afa6", priority: { id: 1, name: "High" } },
    },
});
