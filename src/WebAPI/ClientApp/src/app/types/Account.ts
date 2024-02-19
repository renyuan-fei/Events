export interface RegisterRequest {
    confirmPassword: string;
    displayName: string;
    email: string;
    password: string;
    phoneNumber: string;
}

export interface LoginRequest {
    email: string;
    password: string;
}

export interface AuthResponse {
    displayName: string;
    email: string;
    token: string;
    expirationDateTime: string;
    image: string;
}

export interface User{
    id: string;
    displayName: string;
    // email: string;
    image: string;
}