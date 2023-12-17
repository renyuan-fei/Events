import {ComponentPreview, Previews} from "@react-buddy/ide-toolbox";
import {PaletteTree} from "./palette";
import {NavBar} from "@ui/NavBar.tsx";
import SearchComponent from "@ui/Search.tsx";
import {Logo} from "@ui/Logo.tsx";
import ActivityCard from "@ui/ActivityCard.tsx";

const ComponentPreviews = () => {
    return (
        <Previews palette={<PaletteTree/>}>
            <ComponentPreview path="/NavBar">
                <NavBar/>
            </ComponentPreview>
            <ComponentPreview
                path="/SearchComponent">
                <SearchComponent/>
            </ComponentPreview>
            <ComponentPreview path="/Logo">
                <Logo/>
            </ComponentPreview>
            <ComponentPreview
                path="/ActivityCard">
                <ActivityCard/>
            </ComponentPreview>
        </Previews>
    );
};

export default ComponentPreviews;