import {useParams} from "react-router";

export const AllPhotosPage = () => {
    const {Id} = useParams<{Id:string}>();

    console.log(Id);

    return (
        <>
        </>
    );
};