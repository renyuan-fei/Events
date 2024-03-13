import {Middleware} from "redux";
import {HubConnection, HubConnectionBuilder, LogLevel} from "@microsoft/signalr";
import {
    clearNotifications,
    loadUnreadNotificationCount,
    receiveNotification,
    setInitialTimestamp, setIsConnection, setNotificationStatusRead,
    setPaginatedNotifications,
    updateUnreadNotificationCount
} from "@features/notification/NotificationSlice.ts";
import {NotificationMessage} from "@type/NotificationMessage.ts";
import {PaginatedResponse} from "@type/PaginatedResponse.ts";

//TODO receive new notification
//TODO update notification status
export enum SignalRNotificationActionTypes {
    START_NOTIFICATION_CONNECTION = 'START_NOTIFICATION_CONNECTION',
    STOP_NOTIFICATION_CONNECTION = 'STOP_NOTIFICATION_CONNECTION',
    UPDATE_NOTIFICATION_STATUS = 'UPDATE_NOTIFICATION_STATUS',
    LOAD_PAGINATED_NOTIFICATIONS = 'LOAD_PAGINATED_NOTIFICATIONS',
}

const startNotificationConnection = () => ({
    type: SignalRNotificationActionTypes.START_NOTIFICATION_CONNECTION,
});

const stopNotificationConnection = () => ({
    type: SignalRNotificationActionTypes.STOP_NOTIFICATION_CONNECTION,
});

const updateNotificationStatus = (userNotificationId: string) => ({
    type: SignalRNotificationActionTypes.UPDATE_NOTIFICATION_STATUS,
    payload: userNotificationId,
});

const loadPaginatedNotifications = () => ({
    type: SignalRNotificationActionTypes.LOAD_PAGINATED_NOTIFICATIONS,
});

// BASE_URL for SignalR Hub connection
const BASE_URL = import.meta.env.VITE_API_BASE_URL;

const NotificationHubSignalRMiddleware = (): Middleware => {
    let connection: HubConnection | null = null;

    return store => next => action => {
        switch (action.type) {
            case SignalRNotificationActionTypes.START_NOTIFICATION_CONNECTION:
                if (connection === null) {
                    connection = new HubConnectionBuilder()
                        .withUrl(BASE_URL + "/notifications", {
                            accessTokenFactory: () => localStorage.getItem('jwt') ?? ''
                        })
                        .configureLogging(LogLevel.Information)
                        .withAutomaticReconnect()
                        .build();

                    connection.on('UpdateUnreadNotificationNumber', (r) => {
                        console.log(r);
                        store.dispatch(updateUnreadNotificationCount(r))
                    })

                    connection.on("LoadUnreadNotificationNumber", (notificationNumber: number) => {
                        console.log(notificationNumber);
                        store.dispatch(loadUnreadNotificationCount(notificationNumber))
                    });

                    // Define how to handle incoming notifications
                    connection.on('ReceiveNotificationMessage', (notification: NotificationMessage) => {
                        console.log(notification);
                        store.dispatch(receiveNotification(notification));
                    });

                    connection.on('LoadPaginatedNotifications', (paginatedNotifications: PaginatedResponse<NotificationMessage>) => {
                        console.log(paginatedNotifications);
                        store.dispatch(setPaginatedNotifications(paginatedNotifications));
                    })

                    // Start the connection
                    connection.start()
                        .then(() => {
                            console.log('Notification Hub connection started')
                            store.dispatch((setIsConnection(true)));
                        })
                        .catch(err => console.error('Error while establishing Notification Hub connection:', err));
                }
                break;
            case SignalRNotificationActionTypes.UPDATE_NOTIFICATION_STATUS:
                if (connection!== null) {
                    connection.invoke('ReadNotification', action.payload).then(() => {
                        // 假设后端成功处理了标记为已读的请求
                        // 更新本地Redux状态，标记通知为已读
                        store.dispatch(setNotificationStatusRead(action.payload));
                    })
                        .catch(err => console.error('Error while updating notification status:', err));
                }
                break;
            case SignalRNotificationActionTypes.LOAD_PAGINATED_NOTIFICATIONS:
                if (connection !== null) {
                    const pageNumber = store.getState().notification.pageNumber;
                    const pageSize = store.getState().notification.pageSize;

                    let initialTimestamp;

                    // 如果pageNumber为1，使用当前时间作为timestamp
                    // 否则，使用Redux状态中的initialTimestamp
                    if (pageNumber === 1) {
                        initialTimestamp = new Date().toISOString();
                        store.dispatch(setInitialTimestamp(initialTimestamp));
                    } else {
                        initialTimestamp = store.getState().notification.initialTimestamp;
                    }

                    connection.invoke('GetPaginatedNotifications', pageNumber, pageSize, initialTimestamp)
                }
                break;
            case SignalRNotificationActionTypes.STOP_NOTIFICATION_CONNECTION:
                if (connection) {
                    connection.stop()
                        .then(() => {
                            console.log('Notification Hub connection stopped');
                            store.dispatch(clearNotifications());
                        })
                        .catch(err => console.error('Error while stopping Notification Hub connection:', err));
                    connection = null;
                }
                break;
            default:
                return next(action);
        }
    };
};

export {
    NotificationHubSignalRMiddleware,
    startNotificationConnection,
    stopNotificationConnection,
    updateNotificationStatus,
    loadPaginatedNotifications
};

