# N5 - API 
Proyecto demo de una API en ASP.NET utilizando SQL Server, ElasticSearch y Apache Kafka.

## Instalación - Local
Ejecutar en cmd
```bash
docker-compose up

## Ejecutar - Local
Para ejecutar la API en ambiente Local comentarizar
```yaml
#api:
#  container_name: api
#  build:
#    context: .
#    dockerfile: Dockerfile
#  ports:
#    - "8080:7203"
#  depends_on:
#    - elasticsearch
#    - sql-server
#    - zookeeper
#  environment:
#    - ASPNETCORE_ENVIRONMENT=Development
#    - ASPNETCORE_URLS=http://+:7203

Ejecutar en cmd
```bash
docker-compose up