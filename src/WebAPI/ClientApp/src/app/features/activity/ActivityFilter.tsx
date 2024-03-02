import {Grid} from "@mui/material";
import Box from "@mui/material/Box";
import Button from "@mui/material/Button";
import CustomSelect from "@ui/Custom/CustomSelect.tsx";
import Divider from "@mui/material/Divider";
import {Category} from "@type/Category.ts";
import {useNavigate} from "react-router";
import React from "react";

const ActivityFilter = () => {
    const initialCategoryValues = Object.values(Category);
    const navigate = useNavigate();
    const searchParams = new URLSearchParams(location.search);


    const handleClear = (e : React.MouseEvent<HTMLButtonElement, MouseEvent>) => {
        e.preventDefault();

        if (searchParams.has('page')) {
            searchParams.set('page', '1');
        }
        if (searchParams.has('pageSize')) {
            searchParams.set('pageSize', '8');
        }
        searchParams.delete('category');
        searchParams.delete('startdate');

        navigate({ search: searchParams.toString() });
    }

    const handleHost = (e : React.MouseEvent<HTMLButtonElement, MouseEvent>) => {
        e.preventDefault();

        if (searchParams.has('page')) {
            searchParams.set('page', '1');
        }
        if (searchParams.has('pageSize')) {
            searchParams.set('pageSize', '8');
        }

        searchParams.delete('category');
        searchParams.delete('startdate');

        searchParams.set('ishost', 'true');

        navigate({ search: searchParams.toString() });
    }

    const handleGoing = (e : React.MouseEvent<HTMLButtonElement, MouseEvent>) => {
        e.preventDefault();

        if (searchParams.has('page')) {
            searchParams.set('page', '1');
        }
        if (searchParams.has('pageSize')) {
            searchParams.set('pageSize', '8');
        }

        searchParams.set('isGoing', 'true');

        navigate({ search: searchParams.toString() });
    }

    return (
        <Box sx={{
            width: '100%', // Ensures the container takes full width
        }}>
            <Grid container>
                <Grid item xs={5}>
                    <CustomSelect type={"category"} value={initialCategoryValues} defaultValue={''}/>
                </Grid>
                <Grid item xs={2}>
                    <Button onClick={handleHost} variant={"text"} color={"primary"}>
                        Host
                    </Button>
                </Grid>
                <Grid item xs={2}>
                    <Button onClick={handleGoing} variant={"text"} color={"primary"}>
                        Going
                    </Button>
                </Grid>
                <Grid item xs={3}>
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