import {ComponentPreview, Previews} from "@react-buddy/ide-toolbox";
import {PaletteTree} from "./palette";
import Intro from "@ui/Intro.tsx";
import App from "../App.tsx";
import ActivityCard from "@ui/ActivityCard.tsx";
import SignUpPopup from "@ui/SignUpModal.tsx";

const ComponentPreviews = () => {
    return (
        <Previews palette={<PaletteTree/>}>
            <ComponentPreview path="/Intro">
                <Intro/>
            </ComponentPreview>
            <ComponentPreview path="/App">
                <App/>
            </ComponentPreview>
            <ComponentPreview
                path="/ActivityCard">
                <ActivityCard/>
            </ComponentPreview>
            <ComponentPreview
                path="/SignUpPopup">
                <SignUpPopup/>
            </ComponentPreview>
        </Previews>
    );
};

export default ComponentPreviews;