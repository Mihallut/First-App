import { Board } from "./app/shared/models/board/board.model";
import { BoardState } from "./app/store/boards/boards.state";

export interface AppState {
    isLoading: boolean;
    boards: BoardState;
    error: any | null;
    isSuccess: boolean;
}