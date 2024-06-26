import {ComponentPreview, Previews} from "@react-buddy/ide-toolbox";
import {PaletteTree} from "./palette";
import SignUpForm from "@features/user/SignUpForm.tsx";
import LoginForm from "@features/user/LoginForm.tsx";
import Intro from "@ui/Intro.tsx";
import LoadingComponent from "@ui/LoadingComponent.tsx";
import ActivitiesCalendar from "@features/activity/ActivitiesCalendar.tsx";

const ComponentPreviews = () => {
    return (
        <Previews palette={<PaletteTree/>}>
            <ComponentPreview path="/SignUpForm">
                <SignUpForm/>
            </ComponentPreview>
            <ComponentPreview
                path="/SignInModal">
                <LoginForm/>
            </ComponentPreview>
            <ComponentPreview
                path="/LoadingComponent">
                <LoadingComponent/>
            </ComponentPreview>
            <ComponentPreview path="/Intro">
                <Intro/>
            </ComponentPreview>
            <ComponentPreview
                path="/ActivitiesCalendar">
                <ActivitiesCalendar/>
            </ComponentPreview>
            <ComponentPreview
                path="/ComponentPreviews">
                <ComponentPreviews/>
            </ComponentPreview>
        </Previews>
    );
};

export default ComponentPreviews;