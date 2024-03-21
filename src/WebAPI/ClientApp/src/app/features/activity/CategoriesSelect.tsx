import * as React from 'react';
import MenuItem from '@mui/material/MenuItem';
import Select, {SelectChangeEvent} from '@mui/material/Select';
import {useLocation, useNavigate} from "react-router-dom";
import {Category} from "@type/Category.ts";
import {useDispatch, useSelector} from "react-redux";
import {RootState} from "@store/store.ts";
import {setCategory} from "@features/commonSlice.ts";
import FormControl from "@mui/material/FormControl";
import InputLabel from "@mui/material/InputLabel";

const CategoriesSelect: React.FC = () => {
    const initialCategoryValues = Object.values(Category);
    const dispatch = useDispatch();
    const navigate = useNavigate();
    const location = useLocation();
    const searchParams = new URLSearchParams(location.search);
    const {category} = useSelector((state:RootState) => state.common)
    const handleChange = (event: SelectChangeEvent) => {
        const newValue = event.target.value;
        dispatch(setCategory(newValue));
        searchParams.set('category', newValue);

        if (searchParams.has('page')) {
            searchParams.set('page', '1');
        }
        if (searchParams.has('pageSize')) {
            searchParams.set('pageSize', '8');
        }

        navigate({search: searchParams.toString()});
    };

    return (
        <div>
            <FormControl sx={{m: 1, minWidth: 80, width: '90%'}}>
                <InputLabel id='demo-simple-select-autowidth-label'>category</InputLabel>
                <Select
                    labelId='demo-simple-select-autowidth-label'
                    id='demo-simple-select-autowidth'
                    value={category}
                    onChange={handleChange}
                    autoWidth
                    label={'category'}
                >
                    <MenuItem value=''>
                        <em>All</em>
                    </MenuItem>
                    {
                        initialCategoryValues.map((item, index) => (
                            <MenuItem key={index} value={item}>
                                {item.replace(/And/g, ' • ')}
                            </MenuItem>
                        ))
                    }
                </Select>
            </FormControl>
        </div>
    );
}

export default CategoriesSelect;