import httpx

from app.model import Point

study_client = httpx.Client(base_url="http://study-service.default.svc.cluster.local")


def post_study_result(*, study_id: str, points: list[Point]):
    study_client.post(f"/internal/{study_id}/result", json={"points": list(map(lambda p: p.to_json(), points))})