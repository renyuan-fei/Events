import Box from "@mui/material/Box";

interface ImageProps {
    Src: string,
    Alt: string,
    Sx: {},
}

export function ImageComp({Src, Alt, Sx}: ImageProps) {
    return (
        <Box
            component="img"
            sx={Sx}
            src={Src}
            alt={Alt}
        />
    );
}