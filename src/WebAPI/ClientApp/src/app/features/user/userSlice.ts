import {createSlice} from '@reduxjs/toolkit'

type UserState = {
    username: string
    position: {
        latitude: number
        longitude: number
    }
    address: string
    error?: string
}

const initialState: UserState = {
    username: '',
    position: {
        latitude: 0,
        longitude: 0,
    },
    address: '',
}

const userSlice = createSlice({
    name: 'user',
    initialState,
    reducers: {

    },
})


export type {UserState}
export default userSlice.reducer
