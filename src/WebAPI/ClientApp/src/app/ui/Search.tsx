import InputBase from '@mui/material/InputBase';
import IconButton from '@mui/material/IconButton';
import SearchIcon from '@mui/icons-material/Search';
import Divider from "@mui/material/Divider";
import Box from "@mui/material/Box";

function SearchComponent() {
    return (
        <Box
            component="form"
            sx={{
                p: '2px 4px',
                display: 'flex',
                alignItems: 'center',
                width: { xs: '100%', sm: '300px', md: '400px', lg: '600px' },
                height: 55,
                marginTop: 1,
                boxShadow: 'none',
                border: '1px solid #e0e0e0',
                borderRadius: 3,
                transition: 'box-shadow 0.3s', // 添加阴影动画效果
                '&:hover': {
                    boxShadow: '0 4px 20px 0 rgba(0,0,0,0.12)', // 悬停时的阴影效果
                },
            }}

        >

            <InputBase
                sx={{ ml: 1, flex: 1 }}
                placeholder="Search events"
                inputProps={{ 'aria-label': 'search events' }}
            />

            <Divider sx={{ height: 28, m: 0.5 }} orientation="vertical" />

            <InputBase
                sx={{ ml: 1, flex: 1 }}
                placeholder="Neighborhood, city or zip"
                inputProps={{ 'aria-label': 'search google maps' }}
            />

            <Divider sx={{ m: 0.5 }} orientation={"vertical"} flexItem />

            <IconButton
                type="submit"
                sx={{
                    p: '10px',
                    color: '#F65858',
                    transition: 'transform 0.3s', // 添加点击动画效果
                    '&:hover': {
                        transform: 'scale(1.1)', // 悬停时放大按钮
                        // backgroundColor: 'rgba(0,0,0,0.04)', // 悬停时的背景色
                        color: '#3e8da0', // 悬停时的字体颜色
                        backgroundColor: 'transparent', // 保持背景透明或设置您希望的颜色
                    },
                    '&:active': {
                        transform: 'scale(0.9)', // 点击时缩小按钮
                    },
                }}
                aria-label="search"
            >
                <SearchIcon />
            </IconButton>

        </Box>
    );
}

export default SearchComponent;
