#!/bin/bash

# Check if .env file already exists
if [ -f ".env" ]; then
    echo ".env file already exists. Please update it manually if needed."
    exit 1
fi

echo "Please visit [Cloudinary](https://cloudinary.com/users/register/free) to obtain your API key."
read -p "Enter your Cloudinary API key: " api_key

# Validate input
if [ -z "$api_key" ]; then
    echo "API key cannot be empty. Please try again."
    exit 1
fi

# Create .env file and write API key
echo "CLOUDINARY_API_KEY=$api_key" > .env
echo ".env file created successfully."

