import {useEffect} from 'react'
import './App.css'
import {GetActivities} from "./api/Activities.ts";

function App() {

    useEffect(() => {
        GetActivities()
    }, [])

    return (
        <>

        </>
    )
}

export default App
