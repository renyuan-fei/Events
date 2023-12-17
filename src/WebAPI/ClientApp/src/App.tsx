import './App.css'
import {RouterProvider} from "react-router-dom";
import '@fontsource/roboto/300.css';
import '@fontsource/roboto/400.css';
import '@fontsource/roboto/500.css';
import '@fontsource/roboto/700.css';
import {router} from "@config/Router.tsx";
import {QueryClientProvider} from "react-query";
import {queryClient} from "@apis/queryClient.ts";

function App() {
    return (
        <QueryClientProvider client={queryClient}>
            <RouterProvider router={router}/>
        </QueryClientProvider>

    )
}

export default App
