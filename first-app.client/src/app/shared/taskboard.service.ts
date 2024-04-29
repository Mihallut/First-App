import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { environment } from "src/environments/environment";
import { TaskList } from "./models/taskList.model";
import { ActivityLog } from "./models/activity-log.model";
import { HistoryPaged } from "./models/history-paged.model";

@Injectable({
    providedIn: 'root'
})

export class TaskboardService {
    taskLists: TaskList[] = [];
    historyPaged: HistoryPaged = {
        items: [],
        totalItems: 0
    };

    constructor(private http: HttpClient) {
    }

    urlTaskLists: string = environment.apiBaseUrl + "/TaskList"
    urlActivityLog: string = environment.apiBaseUrl + "/ActivityLog"

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
}