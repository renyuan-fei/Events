import {Middleware} from 'redux';
import {HubConnection, HubConnectionBuilder, LogLevel} from '@microsoft/signalr';

import {ChatComments} from "@type/ChatComments.ts";
import {
    clearComments,
    loadComments,
    receiveComments
} from "@features/comment/CommentSlice.ts";
import {setInitialTimestamp} from "@features/comment/CommentSlice.ts";
import {PaginatedResponse} from "@type/PaginatedResponse.ts";

enum SignalRChatActionTypes {
    START_CONNECTION = 'SIGNALR_START_CONNECTION',
    STOP_CONNECTION = 'SIGNALR_STOP_CONNECTION',
    SEND_MESSAGE = 'SIGNALR_SEND_MESSAGE',
    LOAD_PAGINATED_COMMENTS = 'SIGNALR_LOAD_PAGINATED_COMMENTS',
}

// Action 创建函数
const startConnection = (activityId: string) => ({
    type: SignalRChatActionTypes.START_CONNECTION,
    payload: activityId
});

const stopConnection = () => ({type: SignalRChatActionTypes.STOP_CONNECTION});
const sendMessage = (message: string) => ({
    type: SignalRChatActionTypes.SEND_MESSAGE,
    payload: message
});

const loadPaginatedComments = () => ({
    type: SignalRChatActionTypes.LOAD_PAGINATED_COMMENTS,
});

const BASE_URL = import.meta.env.VITE_API_BASE_URL;

// 创建 SignalR 中间件
const ChatHubSignalRMiddleware = (): Middleware => {
    let connection: HubConnection | null = null;

    return store => next => action => {
        switch (action.type) {
            case SignalRChatActionTypes.START_CONNECTION:
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

                    connection.on('LoadComments', (comments: PaginatedResponse<ChatComments>) => {
                        store.dispatch(setInitialTimestamp(new Date().toISOString()));
                        // console.log("LoadComments:", comments);
                        // 确保分发的 action 和 payload 与 slice 中的定义匹配
                        store.dispatch(loadComments(comments));
                    });

                    connection.on('ReceiveComment', (message: string) => {
                        // console.log("ReceiveComment:", message);
                        // 假设 receiveMessage 期望的 payload 是一个字符串
                        store.dispatch(receiveComments(message));
                    });

                    connection.start()
                        .then(() => {
                            // console.log('Connection started')
                        })
                        .catch(err => console.error('Error while establishing connection', err));
                }
                break;
            case SignalRChatActionTypes.LOAD_PAGINATED_COMMENTS:
                const pageNumber = store.getState().comment.pageNumber;
                const pageSize = store.getState().comment.pageSize;
                const initialTimestamp = store.getState().comment.initialTimestamp;

                console.log("LOAD_PAGINATED_COMMENTS:", pageNumber, pageSize, initialTimestamp);

                if (connection !== null) {
                    connection.invoke('LoadPaginatedComments', pageNumber, pageSize, initialTimestamp);
                }
                break;
            case SignalRChatActionTypes.SEND_MESSAGE:
                if (connection) {
                    connection.invoke('SendComment', action.payload)
                        .catch(err => console.error('Error while sending message', err));
                }
                break;
            case SignalRChatActionTypes.STOP_CONNECTION:
                if (connection) {
                    connection.stop()
                        .then(() => {
                            // console.log('Connection stopped')
                        })
                        .catch(err => {
                            console.error('Error while stopping connection', err)
                        });
                    store.dispatch(clearComments());
                    connection = null;
                }
                break;
            default:
                return next(action);
        }
    };
};

export {
    ChatHubSignalRMiddleware,
    startConnection,
    stopConnection,
    sendMessage,
    loadPaginatedComments
};
