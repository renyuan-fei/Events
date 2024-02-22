import DetailContainer from "@features/activity/DetailContainer.tsx";
import DetailPhoto from "@features/activity/DetailPhoto.tsx";
import DetailContext from "@features/activity/DetailContext.tsx";
import React from "react";


interface UploadHook {
    isUploading: boolean;
    upload: (formData: FormData, callbacks: { onSuccess?: () => void }) => void;
}

interface DetailBodyProps {
    imageUrl: string;
    title: string;
    description: string;
    isCurrentUser: boolean;
    uploadHook: UploadHook;
}

const DetailBody: React.FC<DetailBodyProps> = ({
                                                   imageUrl,
                                                   title,
                                                   description,
                                                   isCurrentUser,
                                                   uploadHook
                                               }) => {

    return (
        <DetailContainer>
            <DetailPhoto
                isCurrentUser={isCurrentUser}
                src={imageUrl}
                uploadHook={uploadHook}
            />
            <DetailContext
                title={title}
                description={description}/>
        </DetailContainer>
    );
}

export default DetailBody;