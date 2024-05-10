import { Board } from "src/app/shared/models/board/board.model";

export interface BoardState {
    isLoading: boolean;
    boards: Board[];
    error: any | null;
    isAddingSuccess: boolean;
    isEditingSeccess: boolean;
}