import httpx

storage_client = httpx.Client(base_url="http://storage-service.default.svc.cluster.local")


def get_dataset_contents(*, dataset_id: str):
    response = storage_client.get(f"/internal/datasets/${dataset_id}")

    response.raise_for_status()

    return response.read()
