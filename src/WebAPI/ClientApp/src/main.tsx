import React from 'react'
import ReactDOM from 'react-dom/client'
import './index.css'
import App from "./App.tsx";
import {DevSupport} from "@react-buddy/ide-toolbox";
import {ComponentPreviews, useInitial} from "./dev";
import {CssBaseline} from "@mui/material";

ReactDOM.createRoot(document.getElementById('root')!).render(
    <React.StrictMode>
        <React.Fragment>
            <CssBaseline/>
            <DevSupport ComponentPreviews={ComponentPreviews}
                        useInitialHook={useInitial}
            >
                <App/>
            </DevSupport>
        </React.Fragment>
    </React.StrictMode>,
)
