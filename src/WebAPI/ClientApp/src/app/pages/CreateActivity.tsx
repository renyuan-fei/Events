import {useParams} from "react-router";
import ActivityForm from "@features/activity/ActivityForm.tsx";
// import {NewActivity} from "@type/NewActivity.ts";
import {useGetActivityQuery} from "@features/activity/hooks/useGetActivityQuery.ts";
import LoadingComponent from "@ui/LoadingComponent.tsx";
import {Category} from "@type/Category.ts";

export const CreateActivity = () => {
    const {activityId} = useParams<{ activityId: string }>();
    const {isGettingActivity, activityDetail} = useGetActivityQuery(activityId);

    if (isGettingActivity) {
        return <LoadingComponent/>
    }

    console.log(activityDetail);

    if (activityDetail) {
        return <ActivityForm
            title={activityDetail.title}
            city={activityDetail.city}
            venue={activityDetail.venue}
            description={activityDetail.description}
            date={new Date(activityDetail.date)}
            category={activityDetail!.category}
            isUpdated={true} id={activityId!}/>

    }

    return (
        <ActivityForm title={""}
                      city={""}
                      venue={""}
                      description={""}
                      date={new Date()}
                      category={Category.TravelAndOutdoor}
                      isUpdated={false} id={""}/>
    );
};