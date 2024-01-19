import * as React from 'react';
import InputLabel from '@mui/material/InputLabel';
import MenuItem from '@mui/material/MenuItem';
import FormControl from '@mui/material/FormControl';
import Select, { SelectChangeEvent } from '@mui/material/Select';

interface customSelectProps {
    type: string;
    value: string[];
}

export default function customSelect(props: customSelectProps) {
    const { type, value } = props;
    const [selectedValue, setSelectedValue] = React.useState('');

    const handleChange = (event: SelectChangeEvent) => {
        setSelectedValue(event.target.value);
    };

    return (
        <div>
            <FormControl sx={{ m: 1, minWidth: 80 , width: '90%'}}>
                <InputLabel id="demo-simple-select-autowidth-label">{type}</InputLabel>
                <Select
                    labelId="demo-simple-select-autowidth-label"
                    id="demo-simple-select-autowidth"
                    value={selectedValue}
                    onChange={handleChange}
                    autoWidth
                    label={type}
                >
                    <MenuItem value="">
                        <em>None</em>
                    </MenuItem>
                    {
                        value.map((item, index) => (
                            <MenuItem key={index} value={item}>
                                {item}
                            </MenuItem>
                        ))
                    }
                </Select>
            </FormControl>
        </div>
    );
}