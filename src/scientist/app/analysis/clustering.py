from io import BytesIO

import pandas as pd
from sklearn.cluster import DBSCAN
from sklearn.preprocessing import LabelEncoder, StandardScaler

from app.model import Point


def clusterize_dataset(*, dataset_contents: bytes, columns: list[str]) -> list[Point]:
    data = pd.read_csv(BytesIO(dataset_contents))

    columns = columns if len(columns) > 0 else list(data.columns)

    if len(columns) not in {2, 3}:
        raise ValueError(f"clusterizing {columns} components is not supported")

    for column in columns:
        if pd.api.types.is_object_dtype(data[column]):
            encoder = LabelEncoder()
            data[column] = encoder.fit_transform(data[column])

    scaled_data = StandardScaler().fit_transform(data[columns])

    dbscan = DBSCAN(eps=0.8, min_samples=2)
    dbscan.fit(scaled_data)

    return [Point(cluster_id=cluster_id, coordinates=list(data.iloc[i])) for i, cluster_id in enumerate(dbscan.labels_)]
