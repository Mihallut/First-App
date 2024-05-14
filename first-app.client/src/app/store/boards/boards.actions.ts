import { createAction, props } from "@ngrx/store";
import { Guid } from "guid-typescript";
import { Board } from "src/app/shared/models/board/board.model";
import { CreateBoardModel } from "src/app/shared/models/board/create-board.model";

export const getBoards = createAction('[Boards] Get Boards');
export const getBoardsSuccess = createAction('[Boards] Get Boards Success', props<{ boards: Board[] }>());
export const getBoardsFailture = createAction('[Boards] Get Boards Failture', props<{ error: Error }>());

export const createBoard = createAction('[Board] Create Board', props<{ name: CreateBoardModel }>());
export const createBoardSuccess = createAction('[Board] Create Board Success', props<{ board: Board }>());
export const createBoardFailture = createAction('[Board] Create Board Failture', props<{ error: Error }>());

export const editBoard = createAction('[Board] Edit Board', props<{ boardId: Guid, name: CreateBoardModel }>());
export const editBoardSuccess = createAction('[Board] Edit Board Success', props<{ board: Board }>());
export const editBoardFailture = createAction('[Board] Edit Board Failture', props<{ error: Error }>());

export const deleteBoard = createAction('[Board] Delete Board', props<{ boardId: Guid }>());
export const deleteBoardSuccess = createAction('[Board] Delete Delete Success', props<{ boardId: Guid }>());
export const deleteBoardFailture = createAction('[Board] Delete Delete Failture', props<{ error: Error }>());