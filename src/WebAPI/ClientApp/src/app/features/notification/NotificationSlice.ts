import {createSlice, PayloadAction} from '@reduxjs/toolkit';
import {NotificationMessage} from "@type/NotificationMessage.ts";
import {PaginatedResponse} from "@type/PaginatedResponse.ts";

type NotificationState = {
    notifications: NotificationMessage[];
    unReadNotificationCount: number;
    pageNumber: number;
    totalPages: number;
    totalCount: number;
    hasPreviousPage: boolean;
    hasNextPage: boolean;
};

const initialState: NotificationState = {
    notifications: [],
    unReadNotificationCount: 0,
    pageNumber: 1,
    totalPages: 0,
    totalCount: 0,
    hasPreviousPage: false,
    hasNextPage: false,
};

const notificationSlice = createSlice({
    name: 'notification',
    initialState,
    reducers: {
        loadAllNotifications: (state, action: PayloadAction<NotificationMessage[]>) => {
            state.notifications = action.payload;
        },
        updateUnreadNotificationCount: (state) => {
            state.unReadNotificationCount += 1;
        },
        receiveNotification: (state, action: PayloadAction<NotificationMessage>) => {
            state.notifications.push(action.payload);
        },
        clearNotifications: (state) => {
            state.notifications = [];
            state.unReadNotificationCount = 0; // 清空通知时重置未读计数
        },
        // 添加处理加载未读通知计数的reducer
        loadUnreadNotificationCount: (state, action: PayloadAction<number>) => {
            state.unReadNotificationCount = action.payload;
        },
        loadPaginatedNotifications: (state, action: PayloadAction<PaginatedResponse<NotificationMessage>>) => {
            state.notifications.push(...action.payload.items);
            state.totalPages = action.payload.totalPages;
            state.totalCount = action.payload.totalCount;
            state.pageNumber = action.payload.pageNumber;
            state.hasPreviousPage = action.payload.hasPreviousPage;
            state.hasNextPage = action.payload.hasNextPage;
        },
        setPageNumber: (state, action: PayloadAction<number>) => {
            state.pageNumber = action.payload;
        }

    },
});

export const {
    loadAllNotifications,
    receiveNotification,
    clearNotifications,
    updateUnreadNotificationCount,
    loadUnreadNotificationCount,
} = notificationSlice.actions;

export type {NotificationState};
export default notificationSlice.reducer;
