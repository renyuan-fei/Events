import axios, {
    AxiosError,
    AxiosInstance,
    AxiosRequestConfig,
    AxiosResponse,
} from 'axios';

const BASE_URL = import.meta.env.VITE_API_BASE_URL;

// 定义接口以扩展 Axios 请求配置（如果需要）
// interface CustomAxiosRequestConfig {
//     // 你可以添加任何自定义配置，例如:
//     // apiVersion?: string;
// }

interface CustomAxiosRequestConfig extends AxiosRequestConfig {
    _retry?: boolean;
}

// build a custom axios request interceptor
const apiClient: AxiosInstance = axios.create({
    baseURL: BASE_URL, // 替换为你的 API 基础 URL
    // withCredentials: true,
    headers: {
        'Content-Type': 'application/json',
        // 可以根据需要添加其他默认请求头
    },
    // 你可以在这里添加其他默认配置
});

// 可以添加请求拦截器
apiClient.interceptors.request.use(config => {
    const token = localStorage.getItem('jwt');

    if (token) {
        config.headers.Authorization = `Bearer ${token}`;
    }

    return config;
}, error => {
    // 处理请求错误
    return Promise.reject(error);
});

// 可以添加响应拦截器
apiClient.interceptors.response.use(
    (response: AxiosResponse) => {
        return Promise.resolve(response);
    },
    async (error: AxiosError) => {

        const originalRequest = error.config as CustomAxiosRequestConfig;

        // 检查错误是否为 401 并且这不是用于刷新 token 的请求
        if (error.response?.status === 401 && !originalRequest._retry) {

            originalRequest._retry = true; // 标记这是一次重试请求

            try {
                const response = await axios.get(`/api/Account/refresh`);

                if (response.status === 200) {
                    // 存储新 token 到本地
                    localStorage.setItem('token', response.data.token);

                    // 更新原始请求的头部
                    // 确保 headers 已定义
                    originalRequest.headers = originalRequest.headers || {};
                    originalRequest.headers['Authorization'] = 'Bearer ' + response.data.token;

                    // 如果需要，也更新 Axios 实例的默认头部
                    apiClient.defaults.headers.common['Authorization'] = 'Bearer ' + response.data.token;

                    // 重新发起原始请求
                    return axios(originalRequest);
                }
            } catch (e) {
                // 处理刷新 token 失败的情况
                console.error('Error refreshing token', e);
                return Promise.reject(e);
            }
        }

        //TODO error handle
        if (error.response) {


            // 可以根据不同的状态码做不同的处理
            if (error.response.status === 401) {
                // 未授权逻辑
                console.log('unauthorized');
                return Promise.reject(error.response.data);
            } else if (error.response.status === 404) {
                // 资源未找到逻辑
                console.log('not found');
                return Promise.reject(error.response.data);
            } else {
                // 其他错误
                console.log(error.response.data);
                return Promise.reject(error.response.data);
            }
        }

        // 对于其他响应错误直接返回
        return Promise.reject(error);
    }
);

export default apiClient;
export type ApiClient = typeof apiClient;