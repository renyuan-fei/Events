import {createSlice, PayloadAction} from '@reduxjs/toolkit';
import {NotificationMessage} from "@type/NotificationMessage.ts";
import {PaginatedResponse} from "@type/PaginatedResponse.ts";

type NotificationState = {
    notifications: NotificationMessage[];
    unReadNotificationCount: number;
    initialTimestamp: string;
    pageNumber: number;
    pageSize: number;
    totalPages: number;
    totalCount: number;
    hasPreviousPage: boolean;
    hasNextPage: boolean;
    isConnection: boolean;
};

const initialState: NotificationState = {
    notifications: [],
    initialTimestamp: new Date('9999-12-31T23:59:59Z').toISOString(),
    unReadNotificationCount: 0,
    pageNumber: 1,
    pageSize: 10,
    totalPages: 0,
    totalCount: 0,
    hasPreviousPage: false,
    hasNextPage: false,
    isConnection: false
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
            state.notifications.unshift(action.payload);
        },
        clearNotifications: (state) => {
            state.notifications = [];
            state.unReadNotificationCount = 0; // 清空通知时重置未读计数
        },
        loadUnreadNotificationCount: (state, action: PayloadAction<number>) => {
            state.unReadNotificationCount = action.payload;
        },
        setPaginatedNotifications: (state, action: PayloadAction<PaginatedResponse<NotificationMessage>>) => {
            if (action.payload.pageNumber === 1) {
                state.notifications = action.payload.items;
                state.totalPages = action.payload.totalPages;
                state.totalCount = action.payload.totalCount;
            } else {
                state.notifications.push(...action.payload.items);
            }
            // 更新其他相关状态
            state.pageNumber = action.payload.pageNumber;
            state.hasPreviousPage = action.payload.hasPreviousPage;
            state.hasNextPage = action.payload.hasNextPage;

            if (action.payload.hasNextPage) {
                state.pageNumber += 1;
            }

        },
        setPageNumber: (state, action: PayloadAction<number>) => {
            state.pageNumber = action.payload;
        },
        setInitialTimestamp: (state, action: PayloadAction<string>) => {
            state.initialTimestamp = action.payload;
        },
        setIsConnection: (state, action: PayloadAction<boolean>) => {
            state.isConnection = action.payload;
        },
        reset: (state) => {
            state.notifications = [];
            state.initialTimestamp = new Date('9999-12-31T23:59:59Z').toISOString();
            state.pageNumber = 1;
            state.pageSize = 10;
            state.totalPages = 0;
            state.totalCount = 0;
            state.hasPreviousPage = false;
            state.hasNextPage = false;
        }
    },
});

export const {
    loadAllNotifications,
    receiveNotification,
    clearNotifications,
    updateUnreadNotificationCount,
    loadUnreadNotificationCount,
    setPaginatedNotifications,
    setInitialTimestamp,
    setPageNumber,
    setIsConnection,
    reset
} = notificationSlice.actions;

export type {NotificationState};
export default notificationSlice.reducer;
