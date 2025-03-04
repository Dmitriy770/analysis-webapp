import httpx

storage_client = httpx.Client(base_url="http://storage-service-aspnet-core.business.svc.cluster.local")


def get_dataset_contents(*, dataset_id: str):
    response = storage_client.get(f"/internal/datasets/{dataset_id}")

    response.raise_for_status()

    body = response.read()
    print(body)

    return body
