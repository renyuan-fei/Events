import Card from '@mui/material/Card';
import CardHeader from '@mui/material/CardHeader';
import CardMedia from '@mui/material/CardMedia';
import CardContent from '@mui/material/CardContent';
import Typography from '@mui/material/Typography';
import { red } from '@mui/material/colors';
import Avatar from '@mui/material/Avatar';
import IconButton from '@mui/material/IconButton';
import MoreVertIcon from '@mui/icons-material/MoreVert';
import EventIcon from '@mui/icons-material/Event';
import PeopleIcon from '@mui/icons-material/People';
import Box from '@mui/material/Box';

export default function ActivityCard() {
    return (
        <Card sx={{ maxWidth: 345 }}>
            <CardHeader
                avatar={
                    <Avatar sx={{ bgcolor: red[500] }} aria-label="recipe">
                        {/* 通常这里会显示活动的类型或相关的首字母 */}
                        E
                    </Avatar>
                }
                action={
                    <IconButton aria-label="settings">
                        <MoreVertIcon />
                    </IconButton>
                }
                title="International & Local Friends Culture and Languages Exchange Southbank"
                subheader="Hosted by: International and local language exchange Southbank..."
                titleTypographyProps={{ variant: 'body1', fontWeight: 'bold' }} // 调整标题样式
                subheaderTypographyProps={{ variant: 'caption' }} // 调整副标题样式
            />
            <CardMedia
                component="img"
                height="140"
                image="/path/to/your/image.jpg" // 替换为实际图片路径
                alt="Group picture"
            />
            <CardContent>
                <Box display="flex" alignItems="center" mb={1}>
                    <EventIcon color="action" sx={{ marginRight: '5px' }} />
                    <Typography variant="caption">THU, DEC 21 • 18:30 GMT+10</Typography>
                </Box>
                <Box display="flex" alignItems="center">
                    <PeopleIcon color="action" sx={{ marginRight: '5px' }} />
                    <Typography variant="caption">113 going</Typography>
                    <Box flexGrow={1} />
                    <Typography variant="caption" component="span" sx={{ bgcolor: 'lightgrey', borderRadius: '4px', padding: '2px 5px' }}>
                        Free
                    </Typography>
                </Box>
            </CardContent>
        </Card>
    );
}
