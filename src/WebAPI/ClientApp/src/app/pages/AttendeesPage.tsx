import React from "react";
import {useParams} from "react-router";

const AttendeePage:React.FC = () => {
    const {activityId} = useParams<{ activityId: string }>();

    return (
        <>
            {activityId}
        </>
    );
};

export default AttendeePage;