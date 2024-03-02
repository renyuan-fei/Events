import {FormProvider, SubmitHandler, useForm} from "react-hook-form";
import Card from "@mui/material/Card";
import CardContent from "@mui/material/CardContent";
import {Box, Container, CardHeader, Typography, useTheme, Grid} from "@mui/material";
import FormField from "@ui/FormField.tsx";
import RichTextEditorField from "@ui/RichTextEditorField.tsx";
import RadioGroupField from "@ui/RadioGroupField.tsx";
import {Category} from "@type/Category.ts";
import ImageField from "@ui/ImageField.tsx";
import DatePickerField from "@ui/DatePickerField.tsx";
import {LoadingButton} from "@mui/lab";
import useCreateActivity from "@features/activity/hooks/useCreateActivity.ts";
import {NewActivity} from "@type/NewActivity.ts";
import {useNavigate} from "react-router";
import React from "react";
import {useDispatch} from "react-redux";
import {setAlertInfo} from "@features/commonSlice.ts";
import LoadingComponent from "@ui/LoadingComponent.tsx";
import {zodResolver} from "@hookform/resolvers/zod";
import {z} from "zod";
import useUploadActivityInitialPhotoMutation
    from "@features/activity/hooks/useUploadActivityInitialPhotoMutation.ts";
import useUpdateActivityMutation
    from "@features/activity/hooks/useUpdateActivityMutation.ts";

const activitySchema = z.object({
    title: z.string().min(1, "Title is required"),
    description: z.string().min(1, "Description is required"),
    category: z.nativeEnum(Category),
    date: z.date(),
    city: z.string().min(1, "City is required"),
    venue: z.string().min(1, "Venue is required"),
}).refine((data) => {

    const now = new Date();
    const tomorrow = new Date(now);
    tomorrow.setDate(tomorrow.getDate() + 1);
    tomorrow.setHours(0, 0, 0, 0);

    const inputDate = new Date(data.date);

    return inputDate >= tomorrow;
}, {
    message: "Date must be at least one day after the current date and time",
    path: ["date"],
});

// Function to split the camel case string and make it more human-readable
const splitCamelCase = (s: string): string => {
    return s
        .split(/(?=[A-Z])/) // Split the string at each uppercase letter
        .join(' ') // Join the resulting array into a string, separating each part with a space
        .trim();
};

// Convert Enum to an array of objects suitable for RadioGroupField options
const categoryOptions: {
    value: string;
    label: string;
}[] = Object.keys(Category).map(key => ({
    value: Category[key as keyof typeof Category],
    label: splitCamelCase(key), // Use the splitCamelCase function to format the label
}));

interface ActivityFormProps extends NewActivity {
    isUpdated: boolean;
    id: string;
}

const ActivityForm: React.FC<ActivityFormProps> = (props) => {
    const methods = useForm<NewActivity>({
        resolver: zodResolver(activitySchema),
        defaultValues: props
    });
    const {handleSubmit} = methods
    const dispatch = useDispatch();
    const [selectedFile, setSelectedFile] = React.useState<File | null>(null);
    const navigate = useNavigate();
    const {
        isCreating,
        create
    } = useCreateActivity();
    const {isUploading, upload} = useUploadActivityInitialPhotoMutation();
    const {isUpdating, update} = useUpdateActivityMutation(props.id);
    const theme = useTheme();

    const onSubmit: SubmitHandler<NewActivity> = async (data) => {
        if (!props.isUpdated) {
            try {
                const newActivityId: string = await create(data);

                if (newActivityId) {
                    if (selectedFile) {
                        const formData = new FormData();
                        formData.append('file', selectedFile);

                        await upload({photo: formData, id: newActivityId});
                    }
                    navigate(`/activity/${newActivityId}`);
                }

                dispatch(setAlertInfo({
                    open: true,
                    severity: "success",
                    message: "Successfully created activity",
                }));
            } catch (error) {
                // 处理创建活动失败的情况
                console.error('Activity creation failed', error);
            }
        } else {
            await update(data,{
                onSuccess: () => {
                    navigate(`/activity/${props.id}`);
                }
            });
        }
    };

    const handleFileUpload = (file: File | null) => {
        setSelectedFile(file);
    };

    if (isCreating || isUploading || isUpdating) {
        return <LoadingComponent/>
    }

    return (
        <Container maxWidth='md' sx={{mt: 4}}>
            <Card raised sx={{my: 4, borderRadius: theme.shape.borderRadius}}>
                <CardHeader title={
                    <Typography variant='h4' component='div' sx={{flexGrow: 1}}>
                        Publish your Activity
                    </Typography>
                }/>
                <CardContent>
                    <FormProvider {...methods}>
                        <Box component={'form'}
                             noValidate
                             onSubmit={handleSubmit(onSubmit)}>
                            <Grid container
                                  spacing={2}>
                                <Grid item xs={12}>
                                    <FormField name={"title"}
                                               label={"Title"}/>
                                </Grid>
                                <Grid item xs={12}>
                                    <FormField name={"city"}
                                               label={"City"}/>
                                </Grid>
                                <Grid item xs={12}>
                                    <FormField name={"venue"}
                                               label={"Venue"}/>
                                </Grid>

                                {!props.isUpdated && <Grid item xs={12}>
                                  <ImageField
                                      onFileUploaded={handleFileUpload}
                                  />
                                </Grid>}

                                <Grid item xs={12}>
                                    <RichTextEditorField
                                        name={'description'}
                                        label={"Description"}
                                    />
                                </Grid>

                                <Grid item xs={6}>
                                    <DatePickerField name={'date'} label={'Date'}/>
                                </Grid>
                                <Grid item xs={6}>
                                    <RadioGroupField name={'category'}
                                                     label={'Category'}
                                                     options={categoryOptions}
                                    />
                                </Grid>
                                <Grid item xs={12} sx={{mt: 2}}>
                            <LoadingButton type='submit'
                                           variant='contained'
                                           color='primary'
                                           fullWidth>
                                {props.isUpdated ? 'Update Activity' : 'Upload Activity'}
                            </LoadingButton>
                                </Grid>
                            </Grid>
                        </Box>
                    </FormProvider>
                </CardContent>
            </Card>
        </Container>
    );
};

export default ActivityForm;
