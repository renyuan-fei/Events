import React from 'react';
import { Pagination } from '@mui/material';
import { useDispatch } from 'react-redux';
import {setPageNumber} from "@features/commonSlice.ts";

interface PaginationComponentProps {
    currentPage: number;
    pageCount: number;
}

const PaginationComponent: React.FC<PaginationComponentProps> = ({
                                                                     currentPage,
                                                                     pageCount,
                                                                 }) => {
    const dispatch = useDispatch();

    const handleChange = (event: React.ChangeEvent<unknown>, page: number) => {
        console.log(page);
        event.preventDefault();
        dispatch(setPageNumber(page)); // 使用dispatch设置pageNumber到Redux中
    };

    return (
        <Pagination
            page={currentPage} // 当前页码
            count={pageCount} // 总页数
            variant="outlined"
            onChange={handleChange} // 修正后的事件处理函数
            sx={{
                display: 'flex',
                justifyContent: 'center',
                marginY: 2, // 上下的 margin
            }}
        />
    );
};

export default PaginationComponent;
