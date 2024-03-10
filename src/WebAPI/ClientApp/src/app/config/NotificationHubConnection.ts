import {Middleware} from "redux";
import {HubConnection, HubConnectionBuilder, LogLevel} from "@microsoft/signalr";
import {
    clearNotifications, loadAllNotifications,
    loadUnreadNotificationCount,
    receiveNotification, updateUnreadNotificationCount
} from "@features/notification/NotificationSlice.ts";

//TODO receive new notification
//TODO load all notifications
//TODO update notification status
export enum SignalRNotificationActionTypes {
    START_NOTIFICATION_CONNECTION = 'START_NOTIFICATION_CONNECTION',
    STOP_NOTIFICATION_CONNECTION = 'STOP_NOTIFICATION_CONNECTION',
    LOAD_NOTIFICATIONS = 'LOAD_NOTIFICATIONS',
    UPDATE_NOTIFICATION_STATUS = 'UPDATE_NOTIFICATION_STATUS'
}

const startNotificationConnection = () => ({
    type: SignalRNotificationActionTypes.START_NOTIFICATION_CONNECTION,
});

const stopNotificationConnection = () => ({
    type: SignalRNotificationActionTypes.STOP_NOTIFICATION_CONNECTION,
});

const loadNotifications = () => ({
    type: SignalRNotificationActionTypes.LOAD_NOTIFICATIONS,
});

const updateNotificationStatus = (id: string) => ({
    type: SignalRNotificationActionTypes.UPDATE_NOTIFICATION_STATUS,
    payload: id,
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
                        store.dispatch(updateUnreadNotificationCount())
                    })

                    connection.on("LoadUnreadNotificationNumber", (notificationNumber: number) => {
                        console.log(notificationNumber);
                        store.dispatch(loadUnreadNotificationCount(notificationNumber))
                    });

                    // Define how to handle incoming notifications
                    connection.on('ReceiveNotificationMessage', (notification: Notification) => {
                        console.log(notification);
                        store.dispatch(receiveNotification(notification));
                    });

                    // Start the connection
                    connection.start()
                        .then(() => console.log('Notification Hub connection started'))
                        .catch(err => console.error('Error while establishing Notification Hub connection:', err));
                }
                break;
            case SignalRNotificationActionTypes.LOAD_NOTIFICATIONS:
                if (connection !== null) {
                    connection.invoke('LoadNotification').then((r:Notification[]) => {
                        store.dispatch(loadAllNotifications(r));
                    });
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
    loadNotifications
};

