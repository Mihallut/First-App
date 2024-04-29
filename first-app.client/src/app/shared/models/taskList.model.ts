import { Guid } from "guid-typescript";
import { Card } from "./card.model";

export interface TaskList{
    id : Guid,
    name : string,
    cards : Card[]
}