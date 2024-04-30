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
    urlActivityLog: string = environment.apiBaseUrl + "/ActivityLog"
    urlActivityLogCard: string = environment.apiBaseUrl + "/ActivityLog/card"
    urlCardEdit: string = environment.apiBaseUrl + "/Card/edit"


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

        var _card = editCardForm.value as AddEditCard;

        this.http.patch(this.urlCardEdit + '/' + _cardId, editCardForm.value)
            .subscribe({
                next: res => {
                    if (this.curentOpenedModalCard.title != '') {
                        this.curentOpenedModalCard = res as Card
                        this.getCardHistoryPaged(1, this.curentOpenedModalCard.id);
                    }
                    this.refreshList();
                },
                error: err => {
                    if (err.status == 400) {
                        // Bad Request response
                        this.setFluentValidationErrors(editCardForm, err.error.errors);
                    }
                    console.log(err)
                }
            })
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