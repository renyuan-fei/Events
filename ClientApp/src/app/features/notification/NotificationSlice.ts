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
    isLoading: boolean;
};

const initialState: NotificationState = {
    notifications: [],
    initialTimestamp: new Date('9999-12-31T23:59:59Z').toISOString(),
    unReadNotificationCount: 0,
    pageNumber: 1,
    pageSize: 15,
    totalPages: 0,
    totalCount: 0,
    hasPreviousPage: false,
    hasNextPage: false,
    isConnection: false,
    isLoading: false,
};

const notificationSlice = createSlice({
    name: 'notification',
    initialState,
    reducers: {
        loadAllNotifications: (state, action: PayloadAction<NotificationMessage[]>) => {
            state.notifications = action.payload;
        },
        updateUnreadNotificationCount: (state,action: PayloadAction<number>) => {
            state.unReadNotificationCount += action.payload;
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
        setNotificationStatusRead: (state, action: PayloadAction<string>) => {
            // 查找并更新对应的通知状态为已读
            const index = state.notifications.findIndex(n => n.id === action.payload);
            if (index !== -1) {
                state.notifications[index].status = true; // 假设status为true表示已读
            }
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
        resetNotifications: (state) => {
            state.notifications = [];
            state.initialTimestamp = new Date('9999-12-31T23:59:59Z').toISOString();
            state.pageNumber = 1;
            state.pageSize = 15;
            state.totalPages = 0;
            state.totalCount = 0;
            state.hasPreviousPage = false;
            state.hasNextPage = false;
        },
        startLoadingNotifications: (state) => {
            state.isLoading = true;
        },
        stopLoadingNotifications: (state) => {
            state.isLoading = false;
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
    resetNotifications,
    setNotificationStatusRead,
    startLoadingNotifications,
    stopLoadingNotifications
} = notificationSlice.actions;

export type {NotificationState};
export default notificationSlice.reducer;
