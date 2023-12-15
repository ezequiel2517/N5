# N5 - API 
Proyecto demo de una API en ASP.NET utilizando SQL Server, ElasticSearch y Apache Kafka.

## Instalación - Development
Ejecutar en cmd
```bash
docker-compose up
```

## Ejecutar - Local
Para debug la API en ambiente local comentarizar (no es necesario)
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
```

Ejecutar en cmd
```bash
docker-compose up
```

## Testing
### API
Solicitudes a la ruta:
```
localhost:8080/api/Permissions
````

POST:
```json
{
    "EmployeeForename": "Virginia",
    "EmployeeSurname": "Estevez",
    "PermissionType": 1,
    "PermissionDate": "2023-12-14T12:00:00"
}
```

PUT:
```json
{
    "Id": 1,
    "EmployeeForename": "Analia",
    "EmployeeSurname": "Dutra",
    "PermissionType": 2,
    "PermissionDate": "2023-12-14T12:00:00"
}
```

### ElasticSearch
Para consultar ElasticSearch:
```
localhost:9200\permissions\_search?
```

### ApacheKafka
Consumir de Kafka (CMD):
```bash
docker exec -it kafka kafka-console-consumer --topic methods --bootstrap-server localhost:9092 --from-beginning
```

## Observaciones
- Se agregan tres test básicos para cada método.
- Se utilizaron patrones: Repository, Unit of Work, Mediator, CQRS.
- Se agregó log solamente en el api controller (se podría haber agregado mucho más).
- El servicio **db-init** solo existe para inicializar la DB de SQL Server.
- Se pueden revisar scripts de inicialización de la DB en **./N5/scripts/init.sql**.