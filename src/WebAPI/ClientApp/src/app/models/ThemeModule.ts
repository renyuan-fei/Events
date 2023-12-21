// 扩展 MuiButton 的样式接口
declare module '@mui/material/Button' {
    interface ButtonPropsVariantOverrides {
        joinEvent: true; // 添加自定义变体
    }
}
