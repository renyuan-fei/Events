import {Photo} from "@type/Photo.ts";
import ImageList from "@mui/material/ImageList";
import {Box} from "@mui/material";
import ImageItem from "@ui/ImageItem.tsx";
import React from "react";
import EmptyState from "@ui/Custom/EmptyState.tsx";
import Overlay from "@ui/Overlay.tsx";

interface PhotoListProps {
    photos: Photo[];
    isCurrentUser: boolean;
    isDeleting: boolean;
    deletePhoto: (publicId: string) => Promise<void>;
    remainingCount: number;
    addPhoto: () => void;
}

const PhotoList: React.FC<PhotoListProps> = ({
                                                 photos,
                                                 isCurrentUser,
                                                 isDeleting,
                                                 deletePhoto,
                                                 remainingCount,
                                                 addPhoto,
                                             }) => {
    return (photos.length === 0 ? (
            <EmptyState isCurrentUser={isCurrentUser} onAdd={addPhoto} resourceName={"Photo"}/>
        ) : (
            <ImageList sx={{width: '100%', height: 'auto'}} cols={3}>
                {photos.map((photo, index) => (
                    <Box key={photo.publicId} sx={{position: 'relative'}}>
                        <ImageItem
                            photo={photo}
                            isCurrentUser={isCurrentUser}
                            isLoading={isDeleting}
                            onDelete={deletePhoto}
                        />
                        {index === 5 && remainingCount > 0 &&
                            <Overlay count={remainingCount}/>}
                    </Box>
                ))}
            </ImageList>
        )
    );
}

export default PhotoList;