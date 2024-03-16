FROM envoyproxy/envoy:v1.29.0

COPY src/Gateway/envoy.yaml /etc/envoy.yaml

EXPOSE 80
EXPOSE 8001

CMD ["envoy", "-c", "/etc/envoy.yaml"]