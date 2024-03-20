import React from 'react';
import {useForm, SubmitHandler, FormProvider} from 'react-hook-form';
import {Modal, Box, Typography, IconButton, Divider, useTheme} from '@mui/material';
import {LoadingButton} from '@mui/lab';
import {UserProfile} from "@type/UserProfile.ts";
import {queryClient} from "@apis/queryClient.ts";
import {userInfo} from "@type/UserInfo.ts";
import useUpdateUserProfileMutation
    from "@features/profile/hooks/useUpdateUserProfileMutation.ts";
import {z} from "zod";
import {zodResolver} from "@hookform/resolvers/zod";
import {checkEmailRegistered} from "@apis/Account.ts";
import FormField from "@ui/FormField.tsx";
import Card from "@mui/material/Card";
import CloseIcon from "@mui/icons-material/Close";
import CardContent from "@mui/material/CardContent";

interface UserUpdateModalProps {
    open: boolean;
    handleClose: () => void;
}

const style = {
    position: 'absolute',
    top: '50%',
    left: '50%',
    transform: 'translate(-50%, -50%)',
    width: 400,
    bgcolor: 'background.paper',
    boxShadow: 24,
    p: 4,
};

const UserUpdateModal: React.FC<UserUpdateModalProps> = ({
                                                             open,
                                                             handleClose,
                                                         }) => {
    const theme = useTheme();
    const currentUserId = queryClient.getQueryData<userInfo>("userInfo")?.id;
    const userInfo = queryClient.getQueryData<UserProfile>(["userProfile", currentUserId])!;


    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

    const schema = z.object({
        displayName: z.string().min(1, "This field is required"),
        email: z.string().email("Email address is invalid"),
        phoneNumber: z.string().regex(/^04\d{8}$/, "Phone number must be 04xxxxxxx"),
        bio: z.string().optional(), // 富文本编辑器的内容可能是可选的
    }).refine(async (data) => {
        if (!emailRegex.test(data.email) || data.email === userInfo.email) {
            return true;
        }

        const isRegistered = await checkEmailRegistered(data.email);
        return !isRegistered;
    }, {
        message: "Email is already registered",
        path: ["email"],
    });

    const methods = useForm<UserProfile>({
        resolver: zodResolver(schema),
        defaultValues: userInfo,
    });
    const {handleSubmit} = methods;
    const {isUpdating, update} = useUpdateUserProfileMutation(currentUserId!);

    const updateUserInfo = async (data: UserProfile) => {
        // console.log(data);
        await update(data);
    }

    const onSubmit: SubmitHandler<UserProfile> = async (data) => {
        const updatedData = {
            ...data,
            id: currentUserId!, // 假设这是额外的信息
            image: userInfo.image,
            userName: data.email,
        };

        await updateUserInfo(updatedData);
        handleClose();
    };

    return (
        <Modal
            open={open}
            onClose={handleClose}
            aria-labelledby='modal-modal-title'
            aria-describedby='modal-modal-description'
        >

            <Card sx={style} component='form' onSubmit={handleSubmit(onSubmit)}>
                <IconButton
                    aria-label='close'
                    onClick={handleClose}
                    sx={{
                        position: 'absolute',
                        right: 8,
                        top: 8,
                    }}
                >
                    <CloseIcon/>
                </IconButton>
                <CardContent>
                    <Typography id='modal-modal-title' variant='h4' component='h2' sx={{
                        marginY: 2,
                    }}>
                        Update your profile
                    </Typography>
                    <FormProvider {...methods}>

                        <Box component={'form'} noValidate>
                            <FormField name='displayName'
                                       label='Name'
                                       required/>
                            <FormField name='email'
                                       label='Email'
                                       required/>
                            <FormField name='phoneNumber'
                                       label='Phone Number'/>
                            <FormField name='bio'
                                       label="What's Up"/>
                            <Divider sx={{my: 2}}/>
                            <Box sx={{display: 'flex', justifyContent: 'center'}}>
                                <LoadingButton
                                    type='submit'
                                    loading={isUpdating}
                                    variant='contained'
                                    sx={{width: theme.spacing(30), height: theme.spacing(6)}}
                                    onClick={handleSubmit(onSubmit)}
                                >
                                    Update
                                </LoadingButton>
                            </Box>
                        </Box>
                    </FormProvider>
                </CardContent>
            </Card>
        </Modal>
    );
};

export default UserUpdateModal;