import * as React from 'react';
import InputLabel from '@mui/material/InputLabel';
import MenuItem from '@mui/material/MenuItem';
import FormControl from '@mui/material/FormControl';
import Select, {SelectChangeEvent} from '@mui/material/Select';
import {useLocation, useNavigate} from "react-router-dom";
import {useEffect} from "react";

interface customSelectProps {
    type: string;
    value: string[];
    defaultValue?: string;
}

const CustomSelect: React.FC<customSelectProps> = ({type, value,defaultValue = ''}) => {
    const [selectedValue, setSelectedValue] = React.useState<string>(defaultValue);
    const navigate = useNavigate();
    const location = useLocation();
    const searchParams = new URLSearchParams(location.search);

    useEffect(() => {
        setSelectedValue(defaultValue);
    }, [defaultValue]);
    const handleChange = (event: SelectChangeEvent) => {
        const newValue = event.target.value;
        setSelectedValue(newValue);
        searchParams.set(type, newValue);

        if (searchParams.has('page')) {
            searchParams.set('page', '1');
        }
        if (searchParams.has('pageSize')) {
            searchParams.set('pageSize', '8');
        }

        navigate({ search: searchParams.toString() });
    };

    return (
        <div>
            <FormControl sx={{m: 1, minWidth: 80, width: '90%'}}>
                <InputLabel id='demo-simple-select-autowidth-label'>{type}</InputLabel>
                <Select
                    labelId='demo-simple-select-autowidth-label'
                    id='demo-simple-select-autowidth'
                    value={selectedValue}
                    onChange={handleChange}
                    autoWidth
                    label={type}
                >
                    <MenuItem value=''>
                        <em>All</em>
                    </MenuItem>
                    {
                        value.map((item, index) => (
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

export default CustomSelect;