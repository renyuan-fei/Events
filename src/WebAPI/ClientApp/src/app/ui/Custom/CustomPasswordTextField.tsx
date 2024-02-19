import {IconButton, TextFieldProps} from "@mui/material";
import VisibilityOffIcon from "@mui/icons-material/VisibilityOff";
import VisibilityIcon from '@mui/icons-material/Visibility';
import React, {forwardRef, useState} from "react";
import CustomTextField from "@ui/Custom/CustomTextField.tsx";

// 使用 forwardRef 包装组件以允许传递 ref
const CustomPasswordTextField = forwardRef<HTMLDivElement, TextFieldProps>((props, ref) => {
    const [visible, setVisible] = useState(false);

    function handleIsVisible(event: React.MouseEvent<HTMLButtonElement>) {
        event.preventDefault();
        setVisible(!visible);
    }

    // 组件实现，你可以根据需要自定义这部分
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
            inputRef={ref} // 使用 inputRef 属性接收 ref
        />
    );
});

export default CustomPasswordTextField;
