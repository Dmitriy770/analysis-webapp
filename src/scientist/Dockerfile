FROM python:3.9-alpine

WORKDIR /src

RUN ["apk", "add", "build-base"]

ADD requirements.txt .
RUN pip3 install -r requirements.txt

ADD main.py app ./

CMD ["python3", "main.py"]
