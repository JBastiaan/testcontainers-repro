FROM python:3.8-slim-buster AS base

# Update and install required packages
RUN apt-get update && \
    apt-get install -y --no-install-recommends sudo curl git openjdk-11-jre-headless && \
    apt-get clean && \
    rm -rf /var/lib/apt/lists/*

# Add a non-root user
RUN useradd -m -s /bin/bash docker && \
    echo "docker ALL=(ALL) NOPASSWD:ALL" >> /etc/sudoers.d/nopasswd

USER docker

# Install NodeJS and Firebase tools
RUN curl -sL https://deb.nodesource.com/setup_20.x | sudo -E bash - && \
    sudo apt-get install -y nodejs && \
    sudo npm install -g firebase-tools

# Install gcloud command utilities
RUN curl -sSL https://sdk.cloud.google.com > /tmp/gcl && \
    bash /tmp/gcl --install-dir=/home/docker/google-cloud-sdk --disable-prompts

# Set Python version for Google Cloud SDK
ENV CLOUDSDK_PYTHON=python3

ENV PATH $PATH:/home/docker/google-cloud-sdk/bin

WORKDIR /app

# Copy Firebase configuration files
COPY firebase.json .firebaserc startfirebase.json storage.rules /app/

# First run will download the current .jar files for firebase
RUN firebase emulators:exec --project test ls >> /dev/null

EXPOSE 4000 9199

# Set default command to start Firebase emulators
CMD ["firebase", "emulators:start", "--only", "storage", "--project", "demo-endoscopy"]
