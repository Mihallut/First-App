import { ActivityLogType } from "./activity-log-type.model";

export interface ActivityLog {
    changedCardTitle: string,
    creationDate: Date,
    valueBefore: string,
    valueAfter: string,
    changedFieldName: string,
    activityLogType: ActivityLogType
}