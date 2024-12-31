#!/bin/bash

helm upgrade kafka oci://registry-1.docker.io/bitnamicharts/kafka \
    --install \
    --atomic \
    --create-namespace \
    --namespace studies-kafka \
    --values values.yaml \
    --set sasl.client.users[0]="$KAFKA_BROKER_USER" \
    --set sasl.client.passwords[0]="$KAFKA_BROKER_PASSWORD" \
    --set sasl.controller.user="$KAFKA_CONTROLLER_USER" \
    --set sasl.controller.password="$KAFKA_CONTROLLER_PASSWORD" \
    --set sasl.interbroker.user="$KAFKA_INTERBROKER_USER" \
    --set sasl.interbroker.password="$KAFKA_INTERBROKER_PASSWORD"