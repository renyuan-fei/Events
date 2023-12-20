import {defineConfig} from 'vite'
import react from '@vitejs/plugin-react-swc'
import tsconfigPaths from "vite-tsconfig-paths";

// https://vitejs.dev/config/

export default defineConfig(({command, mode, isSsrBuild, isPreview}) => {
    return {
        plugins: [react(),
            tsconfigPaths()],
        base: mode === 'production' ? './' : '/',
        server: {
            // 默认为开发服务器的端口
            // port: mode === 'production' ? 5173 : 5173,
            host: true,
            strictPort: true
        },
        build: {},
        preview:
            {
                port: 5173,
                host: true,
                strictPort: true
            }
    }
})
