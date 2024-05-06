import { Guid } from "guid-typescript";

export interface DeleteDialogData {
    componentName: string;
    componentId: Guid;
    componentType: string;
}