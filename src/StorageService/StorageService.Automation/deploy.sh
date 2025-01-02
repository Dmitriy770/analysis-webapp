#!/bin/bash

helm upgrade storage-service oci://registry-1.docker.io/bitnamicharts/aspnet-core \
    --install \
    --atomic \
    --create-namespace \
    --namespace business \
    --timeout 2m0s \
    --values values.yaml \
    --set appFromExternalRepo.clone.revision="$BRANCH"