import {FormProvider, SubmitHandler, useForm} from "react-hook-form";
import Card from "@mui/material/Card";
import CardContent from "@mui/material/CardContent";
import {Box, Grid, Container, CardHeader, Typography, useTheme} from "@mui/material";
import FormField from "@ui/FormField.tsx";
import RichTextEditorField from "@ui/RichTextEditorField.tsx";
import RadioGroupField from "@ui/RadioGroupField.tsx";
import {Category} from "@type/Category.ts";
import ImageField from "@ui/ImageField.tsx";
import DatePickerField from "@ui/DatePickerField.tsx";
import {LoadingButton} from "@mui/lab";
import useCreateActivity from "@features/activity/hooks/useCreateActivity.ts";
import {NewActivity} from "@type/NewActivity.ts";
import useUploadActivityMainPhotoMutation
    from "@hooks/useUploadActivityMainPhotoMutation.ts";
import {useNavigate} from "react-router";
import React from "react";
import {useDispatch} from "react-redux";
import {setAlertInfo} from "@features/commonSlice.ts";
import LoadingComponent from "@ui/LoadingComponent.tsx";

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


const ActivityForm = () => {
    const methods = useForm<NewActivity>();
    const dispatch = useDispatch();
    const [selectedFile, setSelectedFile] = React.useState<File | null>(null);
    const navigate = useNavigate();
    const {
        isCreating,
        create
    } = useCreateActivity();
    const {isUploading, upload} = useUploadActivityMainPhotoMutation();
    const theme = useTheme();

    const onSubmit: SubmitHandler<NewActivity> = async (data) => {
        console.log(data);
        // 先上传除图片以外的信息
        await create(data)
            .then(async (newActivityId: string) => {
                console.log(newActivityId);

                if (newActivityId) {
                    if (selectedFile != undefined) {

                        const formData = new FormData();
                        formData.append('file', selectedFile!);

                        await upload({photo: formData, id: newActivityId}).then(() => {
                            navigate(`/activity/${newActivityId}`);
                        })

                    }
                } else {
                    navigate(`/activity/${newActivityId}`);
                }

                dispatch(setAlertInfo({
                    open: true,
                    severity: "success",
                    message: "Successfully created activity",
                }))
            }).catch(error => {
                // 处理创建活动失败的情况
                console.error('Activity creation failed', error);
            });
    };

    const handleFileUpload = (file: File | null) => {
        setSelectedFile(file);
    };

    if (isCreating || isUploading) {
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
                             onSubmit={methods.handleSubmit(onSubmit)}>
                            <Grid container
                                  spacing={2}>
                                <Grid item xs={12}>
                                    <FormField name={"title"}
                                               control={methods.control}
                                               errors={methods.formState.errors}
                                               label={"Title"}/>
                                </Grid>
                                <Grid item xs={12}>
                                    <FormField name={"city"}
                                               control={methods.control}
                                               errors={methods.formState.errors}
                                               label={"City"}/>
                                </Grid>
                                <Grid item xs={12}>
                                    <FormField name={"venue"}
                                               control={methods.control}
                                               errors={methods.formState.errors}
                                               label={"Venue"}/>
                                </Grid>

                                <Grid item xs={12}>
                                    <ImageField
                                        onFileUploaded={handleFileUpload}
                                    />
                                </Grid>

                                <Grid item xs={12}>
                                    <RichTextEditorField
                                        label={"Description"}
                                        name={'description'}
                                    />
                                </Grid>

                                <Grid item xs={6}>
                                    <DatePickerField name={'date'} label={'Date'}/>
                                </Grid>
                                <Grid item xs={6}>
                                    <RadioGroupField name={'category'}
                                                     label={'Category'}
                                                     options={categoryOptions}
                                                     defaultValue={"TravelAndOutdoor"}/>
                                </Grid>
                                <Grid item xs={12} sx={{mt: 2}}>
                                    <LoadingButton type='submit'
                                                   variant='contained'
                                                   color='primary'
                                                   fullWidth>
                                        Upload Activity
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
