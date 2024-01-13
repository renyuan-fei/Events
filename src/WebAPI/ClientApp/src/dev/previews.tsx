import {ComponentPreview, Previews} from "@react-buddy/ide-toolbox";
import {PaletteTree} from "./palette";
import SignUpForm from "@features/user/SignUpForm.tsx";
import LoginForm from "@features/user/LoginForm.tsx";
import SvgButton from "@ui/User/SvgButton.tsx";
import {UserBar} from "@ui/User/UserBar.tsx";

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
                path="/Connections">
                <SvgButton/>
            </ComponentPreview>
            <ComponentPreview
                path="/UserBar">
                <UserBar/>
            </ComponentPreview>
        </Previews>
    );
};

export default ComponentPreviews;