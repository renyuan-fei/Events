// index.tsx
import React from 'react'
import ReactDOM from 'react-dom/client'
import './index.css'
import App from "./App";
import { CssBaseline } from "@mui/material";
import store from "@store/store";
import { Provider } from "react-redux";
import { DevSupport } from "@react-buddy/ide-toolbox";
import { ComponentPreviews, useInitial } from "./dev";
import { theme } from "@config/CustomTheme";
import { queryClient } from "@apis/queryClient";
import { ReactQueryDevtools } from "react-query/devtools";
import { ThemeProvider } from "@mui/material/styles";
import { QueryClientProvider } from "react-query";

ReactDOM.createRoot(document.getElementById('root')!).render(
    <React.StrictMode>
        <React.Fragment>
            <Provider store={store}>
                <DevSupport ComponentPreviews={ComponentPreviews} useInitialHook={useInitial}>
                    <ThemeProvider theme={theme}>
                        <CssBaseline />
                        <QueryClientProvider client={queryClient}>
                            <App />
                            <ReactQueryDevtools initialIsOpen={false} />
                        </QueryClientProvider>
                    </ThemeProvider>
                </DevSupport>
            </Provider>
        </React.Fragment>
    </React.StrictMode>
);
