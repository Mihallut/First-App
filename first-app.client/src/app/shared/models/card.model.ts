import { Guid } from "guid-typescript";
import { Priority } from "./priority.model";

export interface Card {
    id: Guid,
    title: string,
    description: string,
    dueDate: Date,
    taskListId: Guid,
    priority: Priority
}