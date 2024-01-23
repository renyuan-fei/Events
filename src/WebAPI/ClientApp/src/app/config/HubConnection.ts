import {Middleware} from 'redux';
import {HubConnection, HubConnectionBuilder, LogLevel} from '@microsoft/signalr';
import {
    clearComments,
    loadComments,
    receiveComments,
} from "@features/CommentSlice.ts";
import {ChatComments} from "@type/ChatComments.ts";

enum SignalRActionTypes {
    START_CONNECTION = 'SIGNALR_START_CONNECTION',
    STOP_CONNECTION = 'SIGNALR_STOP_CONNECTION',
    SEND_MESSAGE = 'SIGNALR_SEND_MESSAGE',
    RECEIVE_MESSAGE = 'SIGNALR_RECEIVE_MESSAGE',
}

// Action 创建函数
const startConnection = (activityId: string) => ({
    type: SignalRActionTypes.START_CONNECTION,
    payload: activityId
});

const stopConnection = () => ({type: SignalRActionTypes.STOP_CONNECTION});
const sendMessage = (message: string) => ({
    type: SignalRActionTypes.SEND_MESSAGE,
    payload: message
});

const BASE_URL = import.meta.env.VITE_API_BASE_URL;

// 创建 SignalR 中间件
const createSignalRMiddleware = (): Middleware => {
    let connection: HubConnection | null = null;

    return store => next => action => {
        switch (action.type) {
            case SignalRActionTypes.START_CONNECTION:
                if (connection === null) {
                    const activityId = action.payload;
                    connection = new HubConnectionBuilder()
                        .withUrl(BASE_URL + '/chat' + '?activityId=' + activityId,
                            {
                                accessTokenFactory: () => localStorage.getItem('jwt') ?? ''
                            })
                        .withAutomaticReconnect()
                        .configureLogging(LogLevel.Information)
                        .build();

                    connection.on('LoadComments', (comments: ChatComments[]) => {
                        console.log("LoadComments:", comments);
                        // 确保分发的 action 和 payload 与 slice 中的定义匹配
                        store.dispatch(loadComments(comments));
                    });

                    connection.on('ReceiveComment', (message: string) => {
                        console.log("ReceiveComment:", message);
                        // 假设 receiveMessage 期望的 payload 是一个字符串
                        store.dispatch(receiveComments(message));
                    });

                    connection.start()
                        .then(() => console.log('Connection started'))
                        .catch(err => console.error('Error while establishing connection', err));
                }
                break;
            case SignalRActionTypes.STOP_CONNECTION:
                if (connection) {
                    connection.stop()
                        .then(() => console.log('Connection stopped'))
                        .catch(err => console.error('Error while stopping connection', err));
                    store.dispatch(clearComments());
                    connection = null;
                }
                break;
            case SignalRActionTypes.SEND_MESSAGE:
                if (connection) {
                    connection.invoke('SendComment', action.payload)
                        .catch(err => console.error('Error while sending message', err));
                }
                break;
            default:
                return next(action);
        }
    };
};

export {createSignalRMiddleware, startConnection, stopConnection, sendMessage};
