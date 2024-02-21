import React, {useState} from 'react';
import {IconButton, ImageListItem} from '@mui/material';
import DeleteIcon from '@mui/icons-material/Delete';
import {Photo} from '@type/Photo';
import ConfirmModal from "@ui/ConfirmModal.tsx";

interface ImageItemProps {
    photo: Photo;
    isCurrentUser: boolean;
    isLoading: boolean;
    onDelete: (publicId: string) => Promise<void>;
}

const ImageItem: React.FC<ImageItemProps> = ({
                                                 photo,
                                                 isCurrentUser,
                                                 onDelete,
                                                 isLoading
                                             }) => {
    const [deleteModalOpen, setDeleteModalOpen] = useState(false);
    const [hover, setHover] = useState(false);

    const handleOpenDeleteModal = () => setDeleteModalOpen(true);
    const handleCloseDeleteModal = () => setDeleteModalOpen(false);
    const handleConfirmDelete = () => {
        onDelete(photo.publicId).then(() => setDeleteModalOpen(false));
    };

    return (
        <>
            <ConfirmModal isLoading={isLoading}
                          open={deleteModalOpen}
                          onClose={handleCloseDeleteModal}
                          onConfirm={handleConfirmDelete}/>

            <ImageListItem
                key={photo.publicId}
                sx={{
                    position: 'relative',
                    '&:hover, &:focus': {
                        '& button': {
                            opacity: 1, // Show the button when hovered/focused
                        },
                    },
                }}
                onMouseEnter={() => setHover(true)} // Set hover state to true when mouse enters
                onMouseLeave={() => setHover(false)} // Set hover state to false when mouse leaves
            >
                <img
                    src={`${photo.url}?w=164&h=150&fit=crop&auto=format`}
                    srcSet={`${photo.url}?w=164&h=150&fit=crop&auto=format&dpr=2 2x`}
                    alt={'photo'}
                    loading='lazy'
                />
                {isCurrentUser && (
                    <IconButton
                        onClick={handleOpenDeleteModal}
                        sx={{
                            position: 'absolute',
                            top: 0,
                            right: 0,
                            color: 'common.white',
                            opacity: 0, // Initially hide the button
                            transition: 'opacity 0.3s ease-in-out', // Animate the opacity
                        }}
                        style={{opacity: hover ? 1 : 0}} // Apply inline style for hover state
                    >
                        <DeleteIcon/>
                    </IconButton>
                )}
            </ImageListItem>
        </>
    );
};

export default ImageItem;
