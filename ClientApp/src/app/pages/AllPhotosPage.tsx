import {useParams} from 'react-router';
import useGetPaginatedPhotosQuery from '@hooks/useGetPaginatedPhotosQuery';
import {ImageList, ImageListItem, Box} from '@mui/material';
import {UploadFile} from "@mui/icons-material";
import PhotoUploadModal from "@ui/PhotoUploadModal.tsx";
import {useState} from "react";
import useUploadActivityPhotoMutation from "@hooks/useUploadActivityPhotoMutation.ts";
import {queryClient} from "@apis/queryClient.ts";
import {userInfo} from "@type/UserInfo.ts";
import useUploadUserPhotoMutation from "@hooks/useUploadUserPhotoMutation.ts";
import ImageItem from "@ui/ImageItem.tsx";
import useDeleteUserPhotoMutation from "@hooks/useDeleteUserPhotoMutation.ts";
import useDeleteActivityPhotoMutation from "@hooks/useDeleteActivityPhotoMutation.ts";

const AllPhotosPage = () => {
    const {id} = useParams<{ id: string }>();
    const userId = queryClient.getQueryData<userInfo>('userInfo')?.id;

    const isCurrentUser = userId === id;

    const uploadHook = isCurrentUser ? useUploadUserPhotoMutation(id!) : useUploadActivityPhotoMutation(id!);
    const {isDeleting, delete: deletePhoto} = isCurrentUser ? useDeleteUserPhotoMutation(id!) : useDeleteActivityPhotoMutation(id!);

    const [isUploadModalOpen, setUploadModalOpen] = useState<boolean>(false);

    const handleDeletePhoto = async (publicId: string) => {
        if (isCurrentUser && !isDeleting) {
            await deletePhoto(publicId);
        }
    }

    const handleOpenModal = () => {
        setUploadModalOpen(true);
    };

    const handleCloseModal = () => {
        setUploadModalOpen(false);
    };

    const {
        photos,
        fetchNextPage,
        hasNextPage,
        isFetchingNextPage
    } = useGetPaginatedPhotosQuery(id!, 20);

    return (
        <>
            <PhotoUploadModal
                open={isUploadModalOpen}
                onClose={handleCloseModal}
                uploadHook={uploadHook}/>
            <ImageList cols={4} gap={8}>
                <ImageListItem style={{
                    border: '2px dashed gray',
                    display: 'flex',
                    alignItems: 'center',
                    justifyContent: 'center'
                }}
                >
                    <Box sx={{textAlign: 'center'}} onClick={handleOpenModal}>
                        <p>Upload Image</p>
                        <UploadFile/>
                    </Box>
                </ImageListItem>
                {photos?.map((photo, index) => (
                    <ImageItem key={photo.publicId || index}
                               photo={photo}
                               isCurrentUser={isCurrentUser}
                               isLoading={isDeleting}
                               onDelete={handleDeletePhoto}/>
                ))}
            </ImageList>
            {isFetchingNextPage && <p>Loading...</p>}
            {hasNextPage && (
                <button onClick={() => fetchNextPage()}>
                    Load More
                </button>
            )}
        </>
    );
};

export default AllPhotosPage;
