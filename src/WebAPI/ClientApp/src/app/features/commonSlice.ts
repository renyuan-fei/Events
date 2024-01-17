import {createSlice} from '@reduxjs/toolkit'

type CommonState = {
    signUpOpen: boolean,
    LoginOpen: boolean,
    isMobile: boolean,
    isLoading: boolean,
    alertInfo: {
        open: boolean,
        severity: 'error' | 'success' | 'info' | 'warning',
        message: string,
    },
}

const initialState: CommonState = {
    signUpOpen: false,
    LoginOpen: false,
    isMobile: false,
    isLoading: false,
    alertInfo: { open: false, severity: "info", message: '' },
}

const commonSlice = createSlice({
    name: 'common',
    initialState,
    reducers: {
        setSignUpForm: (state,action) => {
            state.signUpOpen = action.payload;
        },
        setLoginForm: (state, action) => {
            state.LoginOpen = action.payload;
        },
        setIsMobile: (state, action) => {
            state.isMobile = action.payload;
        },
        setLoading: (state, action) => {
            state.isLoading = action.payload;
        },
        setAlertInfo: (state, action) => {
            state.alertInfo = action.payload;
        },

    },
})

export const {
    setSignUpForm,
    setLoginForm,
    setIsMobile,
    setLoading,
    setAlertInfo,
} = commonSlice.actions;
export type {CommonState}
export default commonSlice.reducer
