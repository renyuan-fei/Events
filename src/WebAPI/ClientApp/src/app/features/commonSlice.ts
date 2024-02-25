import { createSlice, PayloadAction } from '@reduxjs/toolkit';

type CommonState = {
    signUpOpen: boolean,
    LoginOpen: boolean,
    isMobile: boolean,
    isLoading: boolean,
    alertInfo: {
        open: boolean,
        severity?: 'error' | 'success' | 'info' | 'warning',
        message?: string,
    },
};

const initialState: CommonState = {
    signUpOpen: false,
    LoginOpen: false,
    isMobile: false,
    isLoading: false,
    alertInfo: { open: false, severity: "info", message: '' },
};

const commonSlice = createSlice({
    name: 'common',
    initialState,
    reducers: {
        setSignUpForm(state, action: PayloadAction<boolean>) {
            state.signUpOpen = action.payload;
        },
        setLoginForm(state, action: PayloadAction<boolean>) {
            state.LoginOpen = action.payload;
        },
        setIsMobile(state, action: PayloadAction<boolean>) {
            state.isMobile = action.payload;
        },
        setLoading(state, action: PayloadAction<boolean>) {
            state.isLoading = action.payload;
        },
        setAlertInfo(state, action: PayloadAction<{ open: boolean, severity?: 'error' | 'success' | 'info' | 'warning', message?: string }>) {
            state.alertInfo = action.payload;
        }
    },
});

export const {
    setSignUpForm,
    setLoginForm,
    setIsMobile,
    setLoading,
    setAlertInfo,
} = commonSlice.actions;

export default commonSlice.reducer;
