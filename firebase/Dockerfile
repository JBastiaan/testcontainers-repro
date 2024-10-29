# Use a slim Node.js base image
FROM node:20-slim

# Install necessary packages and clean up
RUN apt-get update && \
    apt-get install -y openjdk-17-jre && \
    rm -rf /var/lib/apt/lists/*

# Install Firebase CLI
RUN npm install -g firebase-tools

WORKDIR /app

# Copy Firebase configuration files
COPY firebase.json .firebaserc startfirebase.json storage.rules /app/

# Expose Firebase Storage Emulator Port
EXPOSE 4000 9199

# Command to start the Firebase Storage emulator
CMD ["firebase", "emulators:start", "--only", "storage", "--project", "demo-endoscopy"]
