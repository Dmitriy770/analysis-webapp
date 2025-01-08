#!/bin/bash


sed -i "s\{template-dashboard-token}\'$ASPIRE_DASHBOARD_TOKEN'\g" aspire-dashboard-deployment.yaml

kubectl apply -f aspire-dashboard-deployment.yaml

kubectl apply -f aspire-dashboard-service.yaml