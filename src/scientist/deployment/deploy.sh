#!/bin/bash

export KAFKA_SERVER='kafka.studies-kafka.svc.cluster.local:9092'

cd ./src/scientist/deployment || exit

sed -i "s\{template-scientist-image}\ghcr.io/dmitriy770/analysis-webapp-scientist:$IMAGE_TAG\g" scientist-deployment.yaml

sed -i "s\{template-kafka-server}\'$KAFKA_SERVER'\g" scientist-deployment.yaml
sed -i "s\{template-kafka-user}\'$KAFKA_USER'\g" scientist-deployment.yaml
sed -i "s\{template-kafka-password}\'$KAFKA_PASSWORD'\g" scientist-deployment.yaml

kubectl apply -f scientist-deployment.yaml

kubectl apply -f scientist-service.yaml