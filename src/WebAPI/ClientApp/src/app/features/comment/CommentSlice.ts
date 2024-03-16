import {createSlice, PayloadAction} from '@reduxjs/toolkit';
import {ChatComments} from "@type/ChatComments.ts";
import {PaginatedResponse} from "@type/PaginatedResponse.ts";

type CommentState = {
    comments: ChatComments[];
    initialTimestamp: string;
    pageNumber: number;
    pageSize: number;
    totalPages: number;
    totalCount: number;
    hasPreviousPage: boolean;
    hasNextPage: boolean;
};

const initialState: CommentState = {
    comments: [],
    initialTimestamp: new Date('9999-12-31T23:59:59Z').toISOString(),
    pageNumber: 1,
    pageSize: 15,
    totalPages: 0,
    totalCount: 0,
    hasPreviousPage: false,
    hasNextPage: false,
};

const commentSlice = createSlice({
    name: 'comment',
    initialState,
    reducers: {
        loadComments: (state, action: PayloadAction<PaginatedResponse<ChatComments>>) => {
            if (action.payload.pageNumber === 1) {
                state.comments = action.payload.items;
                state.totalPages = action.payload.totalPages;
                state.totalCount = action.payload.totalCount;
            } else {
                state.comments.push(...action.payload.items);
            }
            // 更新其他相关状态
            state.pageNumber = action.payload.pageNumber;
            state.hasPreviousPage = action.payload.hasPreviousPage;
            state.hasNextPage = action.payload.hasNextPage;

            if (action.payload.hasNextPage) {
                state.pageNumber += 1;
            }
        },
        receiveComments: (state, action) => {
            state.comments.unshift(action.payload);
        },
        clearComments: (state) => {
            state.comments = [];
        },
        setInitialTimestamp: (state, action: PayloadAction<string>) => {
            state.initialTimestamp = action.payload;
        },
        resetComments: (state) => {
            state.comments = [];
            state.initialTimestamp = new Date('9999-12-31T23:59:59Z').toISOString();
            state.pageNumber = 1;
            state.pageSize = 15;
            state.totalPages = 0;
            state.totalCount = 0;
            state.hasPreviousPage = false;
            state.hasNextPage = false;
        }
    },
});

export const {
    loadComments,
    receiveComments,
    clearComments,
    setInitialTimestamp,
    resetComments,
} = commentSlice.actions;

export type {CommentState};
export default commentSlice.reducer;
