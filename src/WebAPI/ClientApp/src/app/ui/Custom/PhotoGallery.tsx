import {useNavigate} from "react-router";
import {Photo} from "@type/Photo.ts";
import React, {useState} from "react";
import PhotoUploadModal from "@ui/PhotoUploadModal.tsx";
import GalleryHeader from "@ui/Custom/GalleryHeader.tsx";
import GalleryButton from "@ui/Custom/GalleryButton.tsx";
import GalleryTitle from "@ui/Custom/GalleryTitle.tsx";
import GalleryContainer from "@ui/Custom/GalleryContainer.tsx";
import GalleryDisplay from "@ui/Custom/GalleryDisplay.tsx";
import PhotoList from "@ui/Custom/PhotoList.tsx";
import useDeleteActivityPhotoMutation from "@hooks/useDeleteActivityPhotoMutation.ts";
import useUploadActivityPhotoMutation from "@hooks/useUploadActivityPhotoMutation.ts";

interface PhotoGalleryProps {
    id: string | undefined;
    isCurrentUser: boolean
    photos: Array<Photo>;
    remainingCount: number;
}

const PhotoGallery: React.FC<PhotoGalleryProps> = (props: PhotoGalleryProps) => {

    const navigate = useNavigate();
    const [isUploadModalOpen, setUploadModalOpen] = useState(false);
    const {id, photos, isCurrentUser, remainingCount} = props;
    const {isDeleting, delete: deletePhoto} = useDeleteActivityPhotoMutation(id!);
    const uploadHook = useUploadActivityPhotoMutation(id!)

    const handleSeeAllClick = () => {
        navigate(`/photos/${id}`);
    };

    const handleDeletePhoto = async (publicId: string) => {
        if (isCurrentUser && !isDeleting) {
            await deletePhoto(publicId);
        }
    }

    const handleAddPhotoClick = () => {
        setUploadModalOpen(true);
    };

    const handleCloseModal = () => {
        setUploadModalOpen(false);
    };

    return (
        <>
            {isCurrentUser && <PhotoUploadModal
                open={isUploadModalOpen}
                onClose={handleCloseModal}
                uploadHook={uploadHook}
            />}

            <GalleryContainer>
                <GalleryHeader>
                    <GalleryTitle title={'Photos'} Count={photos.length}/>
                    {remainingCount > 0 ? (
                        <GalleryButton text='See all' onClick={handleSeeAllClick}/>
                    ) : (
                        isCurrentUser &&
                        <GalleryButton text='Add Photos'
                                       onClick={handleAddPhotoClick}
                                       isAddButton/>
                    )}
                </GalleryHeader>

                <GalleryDisplay>
                    <PhotoList photos={photos}
                               isCurrentUser={isCurrentUser}
                               isDeleting={isDeleting}
                               deletePhoto={handleDeletePhoto}
                               addPhoto={handleAddPhotoClick}
                               remainingCount={remainingCount}
                    />
                </GalleryDisplay>

            </GalleryContainer>
        </>
    );
}

export default PhotoGallery;