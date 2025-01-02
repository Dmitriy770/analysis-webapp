#!/bin/bash

helm upgrade ingress-nginx https://kubernetes.github.io/ingress-nginx \
    --install \
    --atomic \
    --create-namespace \
    --namespace common-infra \
    --values values.yaml \
    --timeout 2m0s