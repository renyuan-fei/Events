import {createSlice} from '@reduxjs/toolkit';
import {ChatComments} from "@type/ChatComments.ts";

type CommentState = {
    comments: ChatComments[];
};

const initialState: CommentState = {
    comments: [],
};

const commentSlice = createSlice({
    name: 'comment',
    initialState,
    reducers: {
        loadComments: (state, action) => {
            state.comments = action.payload;
        },
        receiveComments: (state, action) => {
            state.comments.unshift(action.payload);
        },
        clearComments: (state) => {
            state.comments = [];
        }
    },
});

export const {
    loadComments,
    receiveComments,
    clearComments
} = commentSlice.actions;

export type {CommentState};
export default commentSlice.reducer;
