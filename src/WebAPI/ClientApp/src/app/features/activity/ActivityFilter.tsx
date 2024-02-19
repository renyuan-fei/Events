import {Grid} from "@mui/material";
import Box from "@mui/material/Box";
import Button from "@mui/material/Button";
import CustomSelect from "@ui/Custom/CustomSelect.tsx";
import React from "react";
import Divider from "@mui/material/Divider";

// TODO add status to redux and implement reset
const CategoryValue : string[] = [];
const CityValue: string[] = [];

const ActivityFilter = () => {

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
                    <CustomSelect type={"category"} value={CategoryValue}/>
                </Grid>
                <Grid item xs={5}>
                    <CustomSelect type={"city"} value={CityValue}/>
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