import { createSlice, PayloadAction } from '@reduxjs/toolkit';

type CommonState = {
    pageNumber: number,
    pageSize: number,
    pageCount: number,
    searchTerm: string[],
    searchValue: string,
    orderBy: string,
    filterBy: string,
    signUpOpen: boolean,
    LoginOpen: boolean,
    isMobile: boolean,
    isLoading: boolean,
    alertInfo: {
        open: boolean,
        severity: 'error' | 'success' | 'info' | 'warning',
        message: string,
    },
};

const initialState: CommonState = {
    pageNumber: 1,
    pageSize: 10,
    pageCount: 1,
    searchTerm: [],
    searchValue: '',
    orderBy: '',
    filterBy: '',
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
        setAlertInfo(state, action: PayloadAction<{ open: boolean, severity: 'error' | 'success' | 'info' | 'warning', message: string }>) {
            state.alertInfo = action.payload;
        },
        setPageNumber(state, action: PayloadAction<number>) {
            state.pageNumber = action.payload;
        },
        setPageSize(state, action: PayloadAction<number>) {
            state.pageSize = action.payload;
        },
        setPageCount(state, action: PayloadAction<number>) {
            state.pageCount = action.payload;
        },
        setSearchTerm(state, action: PayloadAction<string[]>) {
            state.searchTerm = action.payload;
        },
        setSearchValue(state, action: PayloadAction<string>) {
            state.searchValue = action.payload;
        },
        setOrderBy(state, action: PayloadAction<string>) {
            state.orderBy = action.payload;
        },
        setFilterBy(state, action: PayloadAction<string>) {
            state.filterBy = action.payload;
        },
        // Reset actions remain unchanged
        resetPagination: (state) => {
            state.pageNumber = initialState.pageNumber;
            state.pageSize = initialState.pageSize;
            state.pageCount = initialState.pageCount;
        },
        resetSearch: (state) => {
            state.searchTerm = initialState.searchTerm;
            state.searchValue = initialState.searchValue;
        },
        resetFilters: (state) => {
            state.orderBy = initialState.orderBy;
            state.filterBy = initialState.filterBy;
        },
    },
});

export const {
    setSignUpForm,
    setLoginForm,
    setIsMobile,
    setLoading,
    setAlertInfo,
    setPageNumber,
    setPageSize,
    setPageCount,
    setSearchTerm,
    setSearchValue,
    setOrderBy,
    setFilterBy,
    resetPagination,
    resetSearch,
    resetFilters,
} = commonSlice.actions;

export default commonSlice.reducer;
