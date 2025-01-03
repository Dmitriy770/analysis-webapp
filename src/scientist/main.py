import json

from kafka import KafkaConsumer
from kafka.errors import NoBrokersAvailable

from app.analysis import clusterize_dataset
from app.api import get_dataset_contents, post_study_result


def main():
    print("initializing consumer")

    try:
        consumer = KafkaConsumer(
            'study_topic',
            bootstrap_servers='192.168.0.9:9092',
            auto_offset_reset="earliest",
            enable_auto_commit=True,
            value_deserializer=lambda x: json.loads(x.decode("utf-8")),
            security_protocol="SASL_PLAINTEXT",
            sasl_mechanism="SCRAM-SHA-512",
            sasl_plain_username='gen_user',
            sasl_plain_password='SnIXh2p6DfP9g-',
        )
    except NoBrokersAvailable:
        print("error: no kafka brokers available")
        exit(1)

    print("consuming messages")

    for message in consumer:
        message_data: dict = message.value

        study_id = message_data["id"]
        dataset_id = message_data["dataset"]["id"]
        dataset_columns = message_data["dataset"]["columns"]

        dataset_contents = get_dataset_contents(dataset_id=dataset_id)
        points = clusterize_dataset(dataset_contents=dataset_contents, columns=dataset_columns)

        post_study_result(study_id=study_id, points=points)


if __name__ == "__main__":
    main()
