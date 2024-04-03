#!/bin/bash

# Check if .env file already exists
if [ -f ".env" ]; then
    echo "Oops! It appears your .env file already exists. If you need to update your API key, please do so manually."
    exit 1
fi

echo "Welcome! To get started, you will need your Cloudinary API key. If you don't have one:"
echo "Please visit [Cloudinary](https://cloudinary.com/users/register/free) to obtain your API key."
read -p "Once you have it, please enter your Cloudinary API key here and press enter: " api_key

# Validate input
if [ -z "$api_key" ]; then
    echo "Your API key cannot be empty. Please run this script again when you have your API key."
    exit 1
fi

# Create .env file and write API key
echo "CLOUDINARY_API_KEY=$api_key" > .env
echo "Great! Your .env file has been created successfully. You can now start using your application."