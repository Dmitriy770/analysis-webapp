#!/bin/bash

helm upgrade ingress-nginx oci://ghcr.io/nginxinc/charts/nginx-ingress \
    --install \
    --atomic \
    --create-namespace \
    --namespace common-infra \
    --values values.yaml \
    --timeout 2m0s