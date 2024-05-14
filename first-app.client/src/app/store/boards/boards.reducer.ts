import { createReducer, on } from "@ngrx/store";
import * as BoardActions from "./boards.actions"
import { BoardState } from "./boards.state";

export const initialState: BoardState = {
    isLoading: false,
    boards: [],
    error: null,
    isAddingSuccess: false,
    isEditingSeccess: false
};

export const BoardReducers = createReducer(
    initialState,
    on(BoardActions.getBoards, (state) => ({ ...state, isLoading: true })),
    on(BoardActions.getBoardsSuccess, (state, action) => ({ ...state, isLoading: false, boards: action.boards, error: null })),
    on(BoardActions.getBoardsFailture, (state, action) => ({ ...state, isLoading: false, error: action.error })),

    on(BoardActions.createBoard, (state, { name }) => ({ ...state, isLoading: true })),
    on(BoardActions.createBoardSuccess, (state, { board }) => ({ ...state, isAddingSuccess: true, boards: [...state.boards, board], error: null })),
    on(BoardActions.createBoardFailture, (state, { error }) => ({ ...state, isAddingSuccess: false, error })),

    on(BoardActions.editBoard, (state, { name }) => ({ ...state, isLoading: true })),
    on(BoardActions.editBoardSuccess, (state, { board }) => {
        const updatedBoards = state.boards.map(b => b.id === board.id ? board : b);
        return { ...state, isEditingSeccess: true, boards: updatedBoards, error: null };
    }),
    on(BoardActions.editBoardFailture, (state, { error }) => ({ ...state, isEditingSeccess: false, error })),

    on(BoardActions.deleteBoard, (state, { boardId }) => ({ ...state, isLoading: true })),
    on(BoardActions.deleteBoardSuccess, (state, { boardId }) => {
        const updatedBoards = state.boards.filter(b => b.id !== boardId);
        return { ...state, boards: updatedBoards, error: null };
    }),
    on(BoardActions.deleteBoardFailture, (state, { error }) => ({ ...state, error }))
)