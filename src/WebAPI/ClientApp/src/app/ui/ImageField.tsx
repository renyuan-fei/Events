import React, { useCallback, useState } from "react";
import {Box, styled} from "@mui/material";
import { useDropzone } from "react-dropzone";
import IconButton from "@mui/material/IconButton";
import DeleteIcon from "@mui/icons-material/Delete";
import {useDispatch} from "react-redux";
import {setAlertInfo} from "@features/commonSlice.ts";

// Define the aspect ratio and minimum dimensions
const aspectRatio = { width: 4, height: 3 };
const minImageWidth = 200; // Minimum image width
const minImageHeight = minImageWidth / aspectRatio.width * aspectRatio.height; // Minimum height based on the aspect ratio

const ImagePreview = styled('img')({
  maxWidth: '100%', // Ensure the image is responsive and does not exceed its container
  maxHeight: '100%',
  objectFit: 'contain', // Ensure the image is contained within its aspect ratio
});

interface ImageFieldProps {
  onFileUploaded: (file: File | null) => void;
}

const ImageField: React.FC<ImageFieldProps> = ({ onFileUploaded }) => {
  const [preview, setPreview] = useState<string | null>(null);
  const dispatch = useDispatch();

  const onDrop = useCallback((acceptedFiles: File[]) => {
    const file = acceptedFiles[0];

    if (file) {
      const image = new Image();
      image.src = URL.createObjectURL(file);
      image.onload = () => {
        // Check if image meets minimum dimensions and aspect ratio
        if (image.width < minImageWidth || image.height < minImageHeight) {
          dispatch(setAlertInfo({
            open: true,
            severity: 'error',
            message: 'Image must be at least'+ minImageWidth + 'x' + minImageHeight + ' pixels',
          }))
          URL.revokeObjectURL(image.src);
        } else {
          setPreview(image.src);
          onFileUploaded(file);
        }
      };
    }
  }, [onFileUploaded, dispatch]);

  const { getRootProps, getInputProps } = useDropzone({
    onDrop,
    accept: { 'image/*': ['.jpeg', '.png', '.gif'] },
    multiple: false,
  });

  const removeImage = (event: React.MouseEvent) => {
    event.stopPropagation();
    setPreview(null);
    onFileUploaded(null);
  };

  return (
      <div {...getRootProps()}>
        <input {...getInputProps()} />
        {!preview ? (
            <Box  style={{ cursor: 'pointer', border: '2px dashed #aaa', padding: '20px', textAlign: 'center' }}>
              Drag your image here or click to select an image
            </Box>
        ) : (
            <div style={{ position: 'relative', width: '100%', paddingTop: '75%' }}> {/* Padding-top controls the aspect ratio */}
              <div style={{ position: 'absolute', top: 0, right: 0, bottom: 0, left: 0 }}>
                <ImagePreview src={preview} alt="Preview" />
                <IconButton
                    onClick={removeImage}
                    style={{ position: 'absolute', right: 0, top: 0 }}
                >
                  <DeleteIcon />
                </IconButton>
              </div>
            </div>
        )}
      </div>
  );
};

export default ImageField;
