import {createSlice} from '@reduxjs/toolkit'

type CommonState = {
    signUpOpen: boolean,
    LoginOpen: boolean,
    isMobile: boolean,
}

const initialState: CommonState = {
    signUpOpen: false,
    LoginOpen: false,
    isMobile: false,
}

const commonSlice = createSlice({
    name: 'common',
    initialState,
    reducers: {
        setSignUp: (state) => {
            state.signUpOpen = !state.signUpOpen;
            state.LoginOpen = false;
        },
        setLogin: (state) => {
            state.LoginOpen = !state.LoginOpen;
            state.signUpOpen = false;
        },
        setIsMobile: (state, action) => {
            state.isMobile = action.payload;
        },

    },
})

export const {
    setSignUp,
    setLogin,
    setIsMobile,
} = commonSlice.actions;
export type {CommonState}
export default commonSlice.reducer
