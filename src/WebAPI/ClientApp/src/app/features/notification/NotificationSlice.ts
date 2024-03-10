import {createSlice, PayloadAction} from '@reduxjs/toolkit';

type NotificationState = {
    notifications: Notification[];
    unReadNotificationCount: number;
};

const initialState: NotificationState = {
    notifications: [],
    unReadNotificationCount: 0,
};

const notificationSlice = createSlice({
    name: 'notification',
    initialState,
    reducers: {
        loadAllNotifications: (state, action: PayloadAction<Notification[]>) => {
            state.notifications = action.payload;
        },
        updateUnreadNotificationCount: (state) => {
            state.unReadNotificationCount += 1;
        },
        receiveNotification: (state, action: PayloadAction<Notification>) => {
            state.notifications.push(action.payload);
        },
        clearNotifications: (state) => {
            state.notifications = [];
            state.unReadNotificationCount = 0; // 清空通知时重置未读计数
        },
        // 添加处理加载未读通知计数的reducer
        loadUnreadNotificationCount: (state, action: PayloadAction<number>) => {
            state.unReadNotificationCount = action.payload;
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
