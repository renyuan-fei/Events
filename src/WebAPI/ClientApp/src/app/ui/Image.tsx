import Box from "@mui/material/Box";

interface ImageProps {
    Src: string,
    Alt: string,
    Sx: {},
}

const ImageComp = ({Src, Alt, Sx}: ImageProps) => {
    return (
        <Box
            component="img"
            sx={Sx}
            src={Src}
            alt={Alt}
        />
    );
}

export default ImageComp;