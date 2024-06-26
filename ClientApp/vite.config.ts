import {defineConfig} from 'vite'
import react from '@vitejs/plugin-react-swc'
import tsconfigPaths from "vite-tsconfig-paths";
import basicSsl from '@vitejs/plugin-basic-ssl'
import ignore from "rollup-plugin-ignore";
// https://vitejs.dev/config/

export default defineConfig(({command, mode, isSsrBuild, isPreview}) => {
    return {
        plugins: [react(),
            basicSsl(),
            tsconfigPaths()],
        base: mode === 'production' ? './' : '/',
        server: {
            port: 5173,
            // 默认为开发服务器的端口
            // port: mode === 'production' ? 5173 : 5173,
            host: true,
            strictPort: true,
        },
        build: {
            rollupOptions: {
                plugins: [
                ],

            }
        },
        preview:
            {
                port: 5174,
                host: true,
                strictPort: true
            }
    }
})
