import { HttpClientModule } from '@angular/common/http';
import { NgModule, isDevMode } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { TaskboardComponent } from './components/taskboard/taskboard.component';
import { AppRoutingModule } from './app-routing.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatMenuModule } from '@angular/material/menu';
import { TaskListComponent } from './components/task-list/task-list.component';
import { CardComponent } from './components/card/card.component';
import { MatCardModule } from '@angular/material/card';
import { MatSelectModule } from '@angular/material/select';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatListModule } from '@angular/material/list';
import { ActivityLogComponent } from './components/activity-log/activity-log.component';
import { CardModalComponent } from './components/card-modal/card-modal.component';
import { MatDialogModule } from '@angular/material/dialog';
import { EditCardModalComponent } from './components/edit-card-modal/edit-card-modal.component';
import { MatInputModule } from '@angular/material/input';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatNativeDateModule } from '@angular/material/core';
import { AutosizeModule } from 'ngx-autosize';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AddCardModalComponent } from './components/add-card-modal/add-card-modal.component';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { DeleteDialogModalComponent } from './components/delete-dialog-modal/delete-dialog-modal.component';
import { BoardListComponent } from './components/board-list/board-list.component';

import { StoreModule } from '@ngrx/store';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import { BoardReducers } from './store/boards/boards.reducer';
import { EffectsModule } from '@ngrx/effects';
import { BoardsEffects } from './store/boards/boards.effects';



@NgModule({
  declarations: [
    AppComponent,
    TaskboardComponent,
    TaskListComponent,
    CardComponent,
    ActivityLogComponent,
    CardModalComponent,
    EditCardModalComponent,
    AddCardModalComponent,
    DeleteDialogModalComponent,
    BoardListComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatToolbarModule,
    MatButtonModule,
    MatIconModule,
    MatMenuModule,
    MatCardModule,
    MatSelectModule,
    MatSidenavModule,
    MatListModule,
    MatDialogModule,
    MatInputModule,
    MatDatepickerModule,
    MatFormFieldModule,
    MatNativeDateModule,
    AutosizeModule,
    FormsModule,
    ReactiveFormsModule,
    MatSnackBarModule,
    StoreModule.forRoot({ boards: BoardReducers }),
    StoreDevtoolsModule.instrument({
      maxAge: 25,
      logOnly: !isDevMode(),
      autoPause: true
    }),
    EffectsModule.forRoot(),
    EffectsModule.forFeature([BoardsEffects])
  ],
  providers: [
  ],
  bootstrap: [AppComponent],
  exports: [
  ]
})
export class AppModule { }
