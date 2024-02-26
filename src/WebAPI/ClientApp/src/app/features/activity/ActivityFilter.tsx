import {Grid} from "@mui/material";
import Box from "@mui/material/Box";
import Button from "@mui/material/Button";
import CustomSelect from "@ui/Custom/CustomSelect.tsx";
import React, {useState} from "react";
import Divider from "@mui/material/Divider";
import {Category} from "@type/Category.ts";

const ActivityFilter = () => {
    const initialCategoryValues = Object.values(Category);
    const [categoryValue] = useState<string[]>(initialCategoryValues);
    const [cityValue] = useState<string[]>([]);

    // TODO use react router to set url params and reset page to 1 each time a filter is changed

    function handleClear(e : React.MouseEvent<HTMLButtonElement, MouseEvent>) {
        e.preventDefault();
        //TODO Clear all filters
        console.log("clear");
    }

    return (
        <Box sx={{
            width: '100%', // Ensures the container takes full width
        }}>
            <Grid container>
                <Grid item xs={5}>
                    <CustomSelect type={"category"} value={categoryValue}/>
                </Grid>
                <Grid item xs={5}>
                    <CustomSelect type={"city"} value={cityValue}/>
                </Grid>
                <Grid item xs={2}>
                    <Button onClick={handleClear} variant={"text"} color={"primary"}>
                        Reset filters
                    </Button>
                </Grid>
            </Grid>
            <Divider sx={{ borderWidth: 1.2, borderColor: 'rgb(162,162,162)' }} />
        </Box>
    );
}

export default ActivityFilter;