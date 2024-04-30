import { Guid } from "guid-typescript";

export interface AddEditCard {
    id: Guid,
    title: string,
    description: string,
    dueDate: Date,
    taskListId: Guid,
    priorityId: number
}