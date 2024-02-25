import { useNavigate, useLocation } from "react-router-dom";
import { Pagination } from '@mui/material';
import React from "react";

interface PaginationComponentProps {
    pageCount: number; // 总页数由父组件传入
}

const PaginationComponent: React.FC<PaginationComponentProps> = ({ pageCount }) => {
    const navigate = useNavigate();
    const location = useLocation();
    const searchParams = new URLSearchParams(location.search);
    const currentPage = parseInt(searchParams.get('page') || '1', 10);

    const handleChange = (event: React.ChangeEvent<unknown>, page: number) => {
        event.preventDefault();
        // 更新URL中的page参数
        searchParams.set('page', page.toString());
        navigate({ search: searchParams.toString() });
    };

    return (
        <Pagination
            page={currentPage}
            count={pageCount}
            variant="outlined"
            onChange={handleChange}
            sx={{
                display: 'flex',
                justifyContent: 'center',
                marginY: 2,
            }}
        />
    );
};

export default PaginationComponent;
