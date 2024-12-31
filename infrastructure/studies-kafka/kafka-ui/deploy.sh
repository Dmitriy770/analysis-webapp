#!/bin/bash

helm repo add kafka-ui https://provectus.github.io/kafka-ui-charts

helm upgrade kafka-ui kafka-ui/kafka-ui \
    --install \
    --atomic \
    --create-namespace \
    --namespace studies-kafka \
    --values values.yaml \
    --set yamlApplicationConfig.kafka.clusters[0].properties.sasl.jaas.config="org.apache.kafka.common.security.scram.ScramLoginModule required username=\"$KAFKA_BROKER_USER\" password=\"$KAFKA_BROKER_PASSWORD\";" \
    --set yamlApplicationConfig.spring.security.user.name="$KAFKA_UI_USER_LOGIN" \
    --set yamlApplicationConfig.spring.security.user.password="$KAFKA_UI_USER_PASSWORD"