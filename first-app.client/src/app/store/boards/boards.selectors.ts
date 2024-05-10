import { createSelector } from "@ngrx/store";
import { AppState } from "src/app.state";

export const selectFeature = (state: AppState) => state.boards

export const boardsSelector = createSelector(selectFeature, (state) => state.boards);

export const errorsSelector = createSelector(selectFeature, (state) => state.error);

export const isLoadingSelector = createSelector(selectFeature, (state) => state.isLoading);

export const isAddingSuccessSelector = createSelector(selectFeature, (state) => state.isAddingSuccess);

export const isEdittingSuccessSelector = createSelector(selectFeature, (state) => state.isEditingSeccess);


