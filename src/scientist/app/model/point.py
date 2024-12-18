from dataclasses import dataclass


@dataclass
class Point:
    cluster_id: int
    coordinates: list[float]

    def to_json(self) -> list[float]:
        return [self.cluster_id, *self.coordinates]
