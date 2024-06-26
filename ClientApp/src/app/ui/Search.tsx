import InputBase from '@mui/material/InputBase';
import IconButton from '@mui/material/IconButton';
import SearchIcon from '@mui/icons-material/Search';
import Divider from "@mui/material/Divider";
import Box from "@mui/material/Box";
import {useNavigate} from "react-router-dom";
import React, {useEffect} from 'react';
import {useDispatch, useSelector} from "react-redux";
import {RootState} from "@store/store";
import {setSearch} from "@features/commonSlice";

const SearchComponent = () => {
    const dispatch = useDispatch();
    const { search } = useSelector((state: RootState) => state.common);
    const navigate = useNavigate();

    useEffect(() => {
        const hash = window.location.hash;
        const queryString = hash.substring(hash.indexOf('?'));
        const searchParams = new URLSearchParams(queryString);
        const urlSearchTerm = searchParams.get('title');

        if (urlSearchTerm !== null) {
            dispatch(setSearch(urlSearchTerm));
        }
    }, []);


    const handleSearch = (event: React.FormEvent<HTMLDivElement>) => {
        event.preventDefault();

        const newSearchParams = new URLSearchParams();
        newSearchParams.set('title', search);  // URLSearchParams 自动处理编码

        // 清理不需要的参数
        newSearchParams.delete('isHost');
        newSearchParams.delete('isGoing');
        newSearchParams.delete('category');

        navigate(`/home?${newSearchParams.toString()}`);
    };

    const handleOnChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        dispatch(setSearch(e.target.value));
    };

    return (
        <Box
            component="form"
            onSubmit={handleSearch}
            sx={{
                p: '2px 4px',
                display: 'flex',
                alignItems: 'center',
                width: {xs: '100%', sm: '300px', md: '350px', lg: '600px',xl:'700px'},
                height: {xs: 40, sm: 45},
                boxShadow: 'none',
                border: '1px solid #e0e0e0',
                borderRadius: 3,
                transition: 'box-shadow 0.3s',
                '&:hover': {
                    boxShadow: '0 4px 20px 0 rgba(0,0,0,0.12)',
                },
            }}
        >
            <InputBase
                sx={{ml: 1, flex: 1}}
                placeholder="Search events"
                value={search}
                inputProps={{'aria-label': 'search events'}}
                onChange={handleOnChange}
            />
            <Divider sx={{height: 28, m: 0.5}} orientation="vertical"/>
            <IconButton
                type="submit"
                sx={{
                    p: '10px',
                    color: '#F65858',
                    transition: 'transform 0.3s',
                    '&:hover': {
                        transform: 'scale(1.1)',
                        color: '#3e8da0',
                        backgroundColor: 'transparent',
                    },
                    '&:active': {
                        transform: 'scale(0.9)',
                    },
                }}
                aria-label="search"
            >
                <SearchIcon/>
            </IconButton>
        </Box>
    );
}

export default SearchComponent;
