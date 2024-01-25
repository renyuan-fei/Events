import {configureStore} from '@reduxjs/toolkit'
import {useDispatch} from 'react-redux'
import userReducer from '@features/user/userSlice';
import commonReducer from '@features/commonSlice';
import {createSignalRMiddleware} from "@config/HubConnection.ts";
import commentSlice from "@features/Comment/CommentSlice.ts";
import activitySlice from "@features/activity/activitySlice.ts";

const signalRMiddleware = createSignalRMiddleware(); // 创建 SignalR 中间件实例

const store = configureStore({
    reducer: {
        activity: activitySlice,
        comment: commentSlice,
        common: commonReducer,
        user: userReducer,
    },
    middleware: (getDefaultMiddleware) =>
        getDefaultMiddleware().concat(signalRMiddleware),
})

// Infer the `RootState` and `AppDispatch` types from the store itself
export type RootState = ReturnType<typeof store.getState>
// Inferred type: {posts: PostsState, comments: CommentsState, users: UsersState}
export type AppDispatch = typeof store.dispatch
export const useAppDispatch: () => AppDispatch = useDispatch // Export a hook that can be reused to resolve types

export default store
