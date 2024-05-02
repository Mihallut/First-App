import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { environment } from "src/environments/environment";
import { TaskList } from "./models/taskList.model";
import { HistoryPaged } from "./models/history-paged.model";
import { Guid } from "guid-typescript";
import { AddEditCard } from "./models/add-edit-card.model";
import { Card } from "./models/card.model";
import { FormGroup } from "@angular/forms";

@Injectable({
    providedIn: 'root'
})

export class TaskboardService {
    taskLists: TaskList[] = [];
    historyPaged: HistoryPaged = {
        items: [],
        totalItems: 0
    };

    activityLogsForCard: HistoryPaged = {
        items: [],
        totalItems: 0
    };

    curentOpenedModalCard: Card

    constructor(private http: HttpClient) {
        this.curentOpenedModalCard = this.initializeEmptyCard();
    }

    initializeEmptyCard() {
        return {
            id: Guid.create(),
            title: '',
            description: '',
            dueDate: new Date(),
            taskListId: Guid.create(),
            priority: {
                id: 0,
                name: ''
            }
        } as Card;
    }

    urlTaskLists: string = environment.apiBaseUrl + "/TaskList"
    urlTaskListDelete: string = environment.apiBaseUrl + "/TaskList/delete"
    urlTaskListAdd: string = environment.apiBaseUrl + "/TaskList/add"
    urlTaskListEdit: string = environment.apiBaseUrl + "/TaskList/edit"


    urlActivityLog: string = environment.apiBaseUrl + "/ActivityLog"
    urlActivityLogCard: string = environment.apiBaseUrl + "/ActivityLog/card"

    urlCardEdit: string = environment.apiBaseUrl + "/Card/edit"
    urlCardAdd: string = environment.apiBaseUrl + "/Card/add"
    urlCardDelete: string = environment.apiBaseUrl + "/Card/delete"


    refreshList() {
        this.http.get(this.urlTaskLists)
            .subscribe({
                next: res => {
                    this.taskLists = res as TaskList[];
                },
                error: err => {
                    console.log(err)
                }
            })
    }

    addTaskList(addTaskListForm: FormGroup) {
        return this.http.post(this.urlTaskListAdd, addTaskListForm.value);
    }

    editTaskList(_taskListId: Guid, addTaskListForm: FormGroup) {
        return this.http.patch(this.urlTaskListEdit + '/' + _taskListId, addTaskListForm.value);
    }

    deleteTaskList(_taskListId: Guid) {
        return this.http.delete(this.urlTaskListDelete + '/' + _taskListId);
    }

    updateHistoryPaged() {
        const body = {
            pageNumber: 1
        }
        this.http.post(this.urlActivityLog, body)
            .subscribe({
                next: res => {
                    var resHP = res as HistoryPaged
                    this.historyPaged = resHP;
                },
                error: err => {
                    console.log(err)
                }
            })
    }

    getHistoryPaged(_pageNumber: number) {
        const body = {
            pageNumber: _pageNumber
        }
        this.http.post(this.urlActivityLog, body)
            .subscribe({
                next: res => {
                    var resHP = res as HistoryPaged
                    this.historyPaged.items = this.historyPaged.items.concat(resHP.items);
                    this.historyPaged.totalItems = resHP.totalItems;
                },
                error: err => {
                    console.log(err)
                }
            })
    }

    getCardHistoryPaged(_pageNumber: number, _cardId: Guid) {
        const body = {
            cardId: _cardId,
            pageNumber: _pageNumber
        }
        this.http.post(this.urlActivityLogCard, body)
            .subscribe({
                next: res => {
                    var resHP = res as HistoryPaged
                    this.activityLogsForCard.items = resHP.items;
                    this.activityLogsForCard.totalItems = resHP.totalItems;
                },
                error: err => {
                    console.log(err)
                }
            })
    }

    editCard(_cardId: Guid, editCardForm: FormGroup) {
        return this.http.patch(this.urlCardEdit + '/' + _cardId, editCardForm.value);
    }

    deleteCard(_cardId: Guid) {
        return this.http.delete(this.urlCardDelete + '/' + _cardId);
    }

    addCard(editCardForm: FormGroup) {
        return this.http.post(this.urlCardAdd, editCardForm.value);
    }

    setFluentValidationErrors(form: FormGroup, err: any): void {
        Object.entries(err).forEach(([key, value]) => {
            const control = form.get(key);
            if (!!control) {
                if (Array.isArray(value)) {
                    value.forEach(value => {
                        control.setErrors({
                            fluentValidationError: (value as Array<string>)
                        });
                    });
                }
            }
        });
    }
}