#!/bin/bash

helm repo add bitnami-repo https://charts.bitnami.com/bitnami

helm upgrade frontend-service bitnami-repo/node \
    --install \
    --atomic \
    --create-namespace \
    --namespace business-logic-services \
    --timeout 4m0s \
    --values values.yaml \
    --set revision="$BRANCH" \
    --set installCommand="['/bin/bash', '\./src/frontend/deployment/init\.sh', '$GITHUB_CLIENT_ID']"