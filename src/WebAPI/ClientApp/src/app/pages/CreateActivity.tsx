import {useParams} from "react-router";
import ActivityForm from "@features/activity/ActivityForm.tsx";

export const CreateActivity = () => {
    const {id} = useParams<{id: string}>();

    if (id) {

    }

    return (
        <ActivityForm/>
    );
};