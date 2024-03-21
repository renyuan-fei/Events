import { createSlice, PayloadAction } from '@reduxjs/toolkit';

type CommonState = {
    signUpOpen: boolean,
    LoginOpen: boolean,
    isMobile: boolean,
    isLoading: boolean,
    startDate: string;
    isHost: boolean;
    isGoing: boolean;
    category: string;
    search: string,
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
    startDate: '',
    isHost: false,
    isGoing: false,
    category: '',
    search: '',
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
        },
        setStartDate(state, action: PayloadAction<string>) {
            state.startDate = action.payload;
        },
        setIsHost(state, action: PayloadAction<boolean>) {
            state.isHost = action.payload;
        },
        setIsGoing(state, action: PayloadAction<boolean>) {
            state.isGoing = action.payload;
        },
        setCategory(state, action: PayloadAction<string>) {
            state.category = action.payload;
        },
        setSearch(state, action: PayloadAction<string>){
            state.search = action.payload
        },
        resetFilters(state) {
            state.startDate = '';
            state.isHost = false;
            state.isGoing = false;
            state.category = '';
        },
    },
});

export const {
    setStartDate,
    setIsHost,
    setIsGoing,
    setCategory,
    resetFilters,
    setSignUpForm,
    setLoginForm,
    setIsMobile,
    setLoading,
    setAlertInfo,
    setSearch
} = commonSlice.actions;

export default commonSlice.reducer;
