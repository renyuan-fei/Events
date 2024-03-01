// RichTextEditorField.tsx
import React from "react";
import { useFormContext, Controller } from "react-hook-form";
import 'react-quill/dist/quill.snow.css';
import RichTextEditor from "@ui/RichTextEditor.tsx";
import {Box, FormLabel} from "@mui/material";
import FormControl from "@mui/material/FormControl";

interface RichTextEditorProps {
    label?: string;
    name: string;
    height?: number;  // height 属性
}

const RichTextEditorField: React.FC<RichTextEditorProps> = ({
                                                                name,
                                                                label,
                                                                height = 400,
                                                            }) => {
    const { control } = useFormContext();

    return (
        <FormControl component="fieldset" sx={{
            width: '100%',
        }}>
            <FormLabel component="legend">{label}</FormLabel>
            <Controller
                name={name}
                control={control}
                render={({ field }) => (
                    <Box sx={{
                        height: height * 0.8,
                    }}>
                        <RichTextEditor
                            {...field}
                            value={field.value}
                        />
                    </Box>
                )}
            />
        </FormControl>
    );
};

export default RichTextEditorField;
