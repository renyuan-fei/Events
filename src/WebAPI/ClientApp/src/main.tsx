import React from 'react'
import ReactDOM from 'react-dom/client'
import './index.css'
import App from "./App.tsx";
import {CssBaseline} from "@mui/material";
import store from "@store/store.ts";
import {Provider} from "react-redux";

ReactDOM.createRoot(document.getElementById('root')!).render(
    <React.StrictMode>
        <React.Fragment>

                <Provider store={store}>
                    <CssBaseline/>

                    <App/>
                </Provider>
        </React.Fragment>
    </React.StrictMode>,
)
