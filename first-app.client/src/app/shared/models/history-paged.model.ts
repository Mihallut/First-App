import { ActivityLog } from "./activity-log/activity-log.model";

export interface HistoryPaged {
    items: ActivityLog[],
    totalItems: number
}