import {configureStore} from '@reduxjs/toolkit'
import {useDispatch} from 'react-redux'
import userReducer from '@features/user/userSlice';
import commonReducer from '@features/commonSlice';
import {ChatHubSignalRMiddleware} from "@config/ChatHubConnection.ts";
import {NotificationHubSignalRMiddleware} from "@config/NotificationHubConnection.ts";
import activitySlice from "@features/activity/activitySlice.ts";
import commentSlice from "@features/comment/CommentSlice.ts";
import notificationSlice from "@features/notification/NotificationSlice.ts";

const chatHubMiddleware = ChatHubSignalRMiddleware(); // 创建 SignalR 中间件实例
const notificationHubMiddleware = NotificationHubSignalRMiddleware();


const store = configureStore({
    reducer: {
        activity: activitySlice,
        comment: commentSlice,
        notification: notificationSlice,
        common: commonReducer,
        user: userReducer,
    },
    middleware: (getDefaultMiddleware) =>
        getDefaultMiddleware().concat(chatHubMiddleware, notificationHubMiddleware),
})

// Infer the `RootState` and `AppDispatch` types from the store itself
export type RootState = ReturnType<typeof store.getState>
// Inferred type: {posts: PostsState, comments: CommentsState, users: UsersState}
export type AppDispatch = typeof store.dispatch
export const useAppDispatch: () => AppDispatch = useDispatch // Export a hook that can be reused to resolve types

export default store
