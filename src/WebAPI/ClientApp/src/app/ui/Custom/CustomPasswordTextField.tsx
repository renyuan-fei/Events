import {IconButton, TextFieldProps} from "@mui/material";
import VisibilityOffIcon from "@mui/icons-material/VisibilityOff";
import VisibilityIcon from '@mui/icons-material/Visibility';
import CustomTextField from "@ui/Custom/CustomTextField.tsx";
import React, { useState } from "react";

export function CustomPasswordTextField(props: TextFieldProps) {
    const [visible, setVisible] = useState(false);

    function handleIsVisible(event: React.MouseEvent<HTMLButtonElement>) {
        event.preventDefault();
        setVisible(!visible);
    }

    return (
        <CustomTextField
            {...props}
            type={visible ? "text" : "password"}
            InputProps={{
                endAdornment:
                    <IconButton onClick={handleIsVisible}>
                        {visible ? <VisibilityIcon/> : <VisibilityOffIcon/>}
                    </IconButton>,
            }}
        />
    );
}