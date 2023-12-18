import {createSlice} from '@reduxjs/toolkit'

type CommonState = {
    signUpOpen: boolean,
    LoginOpen: boolean,
}

const initialState: CommonState = {
    signUpOpen: false,
    LoginOpen: false,
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
    },
})

export const{
    setSignUp,
    setLogin,
} = commonSlice.actions;
export type {CommonState}
export default commonSlice.reducer
