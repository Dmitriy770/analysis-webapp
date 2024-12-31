#!/bin/bash

helm upgrade studies-service oci://registry-1.docker.io/bitnamicharts/aspnet-core \
    --install \
    --atomic \
    --create-namespace \
    --namespace business-logic-services \
    --values values.yaml \
    --set image.tag="$TAG" \
    --set kong.extraEnvVars[0].value="$KAFKA_STUDY_SERVICE_BROKER_USER" \
    --set kong.extraEnvVars[0].value="$KAFKA_STUDY_SERVICE_BROKER_PASSWORD" \
