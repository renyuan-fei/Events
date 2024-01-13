import {createSlice} from '@reduxjs/toolkit'

type UserState = {
    isLogin: boolean,
}

const initialState: UserState = {
    isLogin: false,
}

const userSlice = createSlice({
    name: 'user',
    initialState,
    reducers: {
        registerAction: (state, action) => {
            state.isLogin = true;
            localStorage.setItem('jwt', action.payload.token);
        },
        loginAction: (state, action) => {
            state.isLogin = true;
            localStorage.setItem('jwt', action.payload.token);
        },

        logoutAction: (state) => {
            state.isLogin = false;
            localStorage.removeItem('jwt');
        }
    },
})
export const {
    registerAction,
    loginAction,
    logoutAction,
} = userSlice.actions;

export type {UserState}
export default userSlice.reducer
