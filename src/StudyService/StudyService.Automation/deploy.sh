#!/bin/bash

helm upgrade studies-service oci://registry-1.docker.io/bitnamicharts/aspnet-core \
    --install \
    --atomic \
    --create-namespace \
    --namespace business-logic-services \
    --timeout 2m0s \
    --values values.yaml \
    --set appFromExternalRepo.clone.revision="$BRANCH" \
    --set kong.extraEnvVars[0].value="$KAFKA_STUDY_SERVICE_BROKER_USER" \
    --set kong.extraEnvVars[1].value="$KAFKA_STUDY_SERVICE_BROKER_PASSWORD" \
