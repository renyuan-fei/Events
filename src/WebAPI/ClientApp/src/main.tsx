import React from 'react'
import ReactDOM from 'react-dom/client'
import './index.css'
import App from "./App.tsx";
import {DevSupport} from "@react-buddy/ide-toolbox";
import {ComponentPreviews, useInitial} from "./dev";
import {CssBaseline} from "@mui/material";
import store from "@store/store.ts";
import {Provider} from "react-redux";

ReactDOM.createRoot(document.getElementById('root')!).render(
    <React.StrictMode>
        <React.Fragment>
            <DevSupport ComponentPreviews={ComponentPreviews}
                        useInitialHook={useInitial}
            >
                <Provider store={store}>
                    <CssBaseline/>

                    <App/>
                </Provider>
            </DevSupport>
        </React.Fragment>
    </React.StrictMode>,
)
