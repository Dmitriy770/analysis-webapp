import json
import os

from kafka import KafkaConsumer
from kafka.errors import NoBrokersAvailable

from app.analysis import clusterize_dataset
from app.api import get_dataset_contents, post_study_result


def main():
    print("initializing consumer")
    
    try:
        consumer = KafkaConsumer(
            bootstrap_servers=os.getenv('KAFKA_SERVER'),
            auto_offset_reset="earliest",
            enable_auto_commit=True,
            value_deserializer=lambda x: json.loads(x.decode("utf-8")),
            security_protocol="SASL_PLAINTEXT",
            sasl_mechanism="PLAIN",
            sasl_plain_username=os.getenv('KAFKA_USER'),
            sasl_plain_password=os.getenv('KAFKA_PASSWORD'),
        )
    except NoBrokersAvailable as ex:
        print(f"error: no kafka brokers available.")
        raise

    print("consuming messages")

    consumer.subscribe(['study_topic'])

    for message in consumer:
        message_data: dict = message.value

        study_id = message_data["Id"]
        dataset_id = message_data["DatasetEntity"]["Id"]
        dataset_columns = message_data["DatasetEntity"]["Columns"]

        dataset_contents = get_dataset_contents(dataset_id=dataset_id)
        points = clusterize_dataset(dataset_contents=dataset_contents, columns=dataset_columns)

        print(list(map(lambda p: p.to_json(), points)))

        post_study_result(study_id=study_id, points=points)

    consumer.close()


if __name__ == "__main__":
    main()
