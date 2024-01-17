import {ComponentPreview, Previews} from "@react-buddy/ide-toolbox";
import {PaletteTree} from "./palette";
import SignUpForm from "@features/user/SignUpForm.tsx";
import LoginForm from "@features/user/LoginForm.tsx";
import {LoadingComponent} from "@ui/LoadingComponent.tsx";
import Intro from "@ui/Intro.tsx";

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
        </Previews>
    );
};

export default ComponentPreviews;