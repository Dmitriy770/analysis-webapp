FROM devopsfaith/krakend as builder
ARG ENV=prod

COPY krakend.json /tmp/krakend.json

# The linting needs the final krakend.json file
RUN krakend check -c /tmp/krakend.json --lint

FROM devopsfaith/krakend
COPY --from=builder --chown=krakend /tmp/krakend.json .

EXPOSE 8080