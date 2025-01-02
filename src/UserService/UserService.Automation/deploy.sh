#!/bin/bash

helm upgrade user-service oci://registry-1.docker.io/bitnamicharts/aspnet-core \
    --install \
    --atomic \
    --create-namespace \
    --namespace business-logic-services \
    --timeout 4m0s \
    --values values.yaml \
    --set appFromExternalRepo.clone.revision="$BRANCH" \
    --set kong.extraEnvVars[0].value="$SESSION_STORAGE_USER" \
    --set kong.extraEnvVars[1].value="$SESSION_STORAGE_PASSWORD" \
    --set kong.extraEnvVars[2].value="$USER_STORAGE_USER" \
    --set kong.extraEnvVars[3].value="$USER_STORAGE_PASSWORD" \
    --set kong.extraEnvVars[4].value="$GITHUB_CLIENT_ID" \
    --set kong.extraEnvVars[5].value="$GITHUB_SECRET"
