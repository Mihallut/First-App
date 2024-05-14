import { Injectable } from "@angular/core"
import { Actions, createEffect, ofType } from "@ngrx/effects"
import * as BoardsActions from "./boards.actions"
import { catchError, concatMap, map, mergeMap, of, switchMap, tap } from "rxjs"
import { TaskboardService } from "src/app/shared/taskboard.service"

@Injectable()
export class BoardsEffects {
    $getBoards = createEffect(() =>
        this.actions$.pipe(
            ofType(BoardsActions.getBoards),
            switchMap(() =>
                this.service.getBoards().pipe(
                    map((boards) => BoardsActions.getBoardsSuccess({ boards })),
                    catchError((error) =>
                        of(BoardsActions.getBoardsFailture({ error }))
                    ))
            )
        )
    );

    createBoard$ = createEffect(() =>
        this.actions$.pipe(
            ofType(BoardsActions.createBoard),
            mergeMap((action) =>
                this.service.addBoard(action.name).pipe(
                    map((board) => BoardsActions.createBoardSuccess({ board })),
                    catchError((error) => of(BoardsActions.createBoardFailture({ error })))
                )
            )
        )
    );

    editBoard$ = createEffect(() =>
        this.actions$.pipe(
            ofType(BoardsActions.editBoard),
            mergeMap((action) =>
                this.service.editBoard(action.boardId, action.name).pipe(
                    map((board) => BoardsActions.editBoardSuccess({ board })),
                    catchError((error) => of(BoardsActions.editBoardFailture({ error })))
                )
            )
        )
    );

    deleteBoard$ = createEffect(() =>
        this.actions$.pipe(
            ofType(BoardsActions.deleteBoard),
            mergeMap((action) =>
                this.service.deleteBoard(action.boardId).pipe(
                    map(() => BoardsActions.deleteBoardSuccess({ boardId: action.boardId })),
                    catchError((error) => of(BoardsActions.deleteBoardFailture({ error })))
                )
            )
        )
    );



    constructor(private actions$: Actions, private service: TaskboardService) {

    }

}
