import Box from "@mui/material/Box";
import {Paper, useTheme} from "@mui/material";
import Typography from "@mui/material/Typography";

export function DetailBody() {
    const theme = useTheme();

    const imageUrl = "https://res.cloudinary.com/dxwtrnpqi/image/upload/v1702382811/sample.jpg"
    const title = "Australia Day BBQ & Live Band at Eastwood"
    const description = "connect Borders Australia Afghani core Leu Marketing Keys generation contingency action-items Towels mobile China Nauru Data Forward Salad Agent Dobra quantify Fantastic utilize Plastic card Manager Agent feed Networked multimedia XML Data Kansas invoice quantify optical hacking virtual Towels Forge Utah blockchains green deliverables Loan RAM magnetic ivory Salad wireless synthesizing quantifying Rwanda Health Cambridgeshire Analyst digital embrace software Chile El orchestration Peso Armenia Handmade Berkshire grey mindshare back-end Soap Minnesota transform Georgia Electronics integrated Azerbaijan evolve Computer transmitting Consultant Intuitive RSS Small California array International Card Idaho throughput Ford invoice"


    return (
        <Paper sx={{ marginTop: theme.spacing(2), marginBottom:theme.spacing(5), padding: theme.spacing(2.8)}}>
                <Box
                    component="img"
                    src={imageUrl}
                    sx={{
                        width: '100%',
                        height: 'auto',
                        marginBottom: theme.spacing(2),
                    }}
                    onLoad={(e) => {
                        const target = e.target as HTMLImageElement; // Cast to the correct type
                        const height = target.height;
                        target.style.height = `${height * 0.8}px`; // Set the height to 80% of the loaded image height
                    }}
                />


                <Box component={"div"}>
                    <Typography variant={"h2"} component={"h2"} sx={{
                        fontWeight: theme.typography.fontWeightBold,
                        fontSize: '20px',
                        marginBottom: theme.spacing(2.5),
                    }}>
                        Details
                    </Typography>
                    <Typography variant={"h1"} component={"div"} sx={{
                        fontWeight: theme.typography.fontWeightBold,
                        fontSize: '16px',
                        marginBottom: theme.spacing(0.5),
                    }}>
                        {title}
                    </Typography>
                    <Typography variant={"body1"} component={"strong"} sx={{

                    }}>
                        {description}
                    </Typography>
                </Box>
        </Paper>
    );
}