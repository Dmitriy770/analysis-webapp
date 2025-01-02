#!/bin/bash

helm repo add bitnami-repo https://charts.bitnami.com/bitnami

helm upgrade frontend-service bitnami-repo/node \
    --install \
    --atomic \
    --create-namespace \
    --namespace business-logic-services \
    --values values.yaml \
    --timeout 3m0s