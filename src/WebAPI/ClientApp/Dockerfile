# Stage 1: Build the application
FROM node:18 AS build

# Set the working directory in the Docker container
WORKDIR ./app

# Copy package.json
COPY ["src/WebAPI/ClientApp/package.json", "./"]

# Install dependencies in the container
RUN npm install --legacy-peer-deps -g npm@10.2.5
RUN npm install @rollup/rollup-linux-arm64-gnu --save-optional --legacy-peer-deps

# Copy the rest of the application source code
COPY ["src/WebAPI/ClientApp/","."]

# Build the application
RUN npm run build

# Stage 2: Serve the application from Nginx
FROM nginx:stable-alpine

# Copy the built app to Nginx's serve directory
COPY --from=build ["/app/dist","/usr/share/nginx/html"]

# Expose port 5173 to the outside once the container has launched
EXPOSE 80

# Start Nginx and keep it running in the foreground
CMD ["nginx", "-g", "daemon off;"]
