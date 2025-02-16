# # Stage 1: Build React app with Vite
# FROM node:20 AS build
# WORKDIR /app

# # Copy package files and install dependencies using npm ci for a clean install
# COPY package.json package-lock.json ./
# RUN npm ci

# # Copy the rest of the source code and build the app
# COPY . .
# RUN npm run build

# # Stage 2: Serve with Nginx
# FROM nginx:alpine
# COPY --from=build /app/dist /usr/share/nginx/html

# # Expose port 80 for serving the app
# EXPOSE 80

# # Start Nginx in the foreground
# CMD ["nginx", "-g", "daemon off;"]
# Stage 1: Build React app with Vite
# Stage 1: Build React app with Vite
FROM node:20 AS build
WORKDIR /app
COPY package.json package-lock.json ./
RUN npm ci
COPY . .
RUN npm run build

# Stage 2: Serve with Nginx
FROM nginx:alpine
COPY --from=build /app/dist /usr/share/nginx/html
# Copy and configure entrypoint
COPY entrypoint.sh /entrypoint.sh
RUN chmod +x /entrypoint.sh
ENTRYPOINT ["/entrypoint.sh"]