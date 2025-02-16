#!/bin/sh
# Generate runtime config file
echo "window._env_ = {" > /usr/share/nginx/html/config.js
echo "  API_URL: \"${API_URL}\"" >> /usr/share/nginx/html/config.js
echo "}" >> /usr/share/nginx/html/config.js
# Start NGINX
exec nginx -g 'daemon off;'