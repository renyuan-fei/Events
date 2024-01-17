import {createSlice} from '@reduxjs/toolkit'

type UserState = {
    isLogin: boolean,
    userName: string,
    imageUrl: string,
}

const initialState: UserState = {
    isLogin: false,
    userName: '',
    imageUrl: '',
}

const userSlice = createSlice({
    name: 'user',
    initialState,
    reducers: {
        loginAction: (state, action) => {
            state.isLogin = true;
            state.userName = action.payload.userName;
            state.imageUrl = action.payload.imageUrl;
            localStorage.setItem('jwt', action.payload.token);
        },

        logoutAction: (state) => {
            state.isLogin = false;
            state.userName = '';
            state.imageUrl = '';
            localStorage.removeItem('jwt');
        }
    },
})
export const {
    loginAction,
    logoutAction,
} = userSlice.actions;

export type {UserState}
export default userSlice.reducer
