import { ActivityLog } from "./activity-log.model";

export interface HistoryPaged {
    items: ActivityLog[],
    totalItems: number
}