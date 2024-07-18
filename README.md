# Features: 

- Clean Architecture
- Api Versioning
- Swagger
- Automapper
- Entity Framework Core
- App Insights
- Health Check
- JWT validation
- CORS config
- Docker

# To Do

- Do Full CRUD
- Unit Test
- Functional Tests
- CI/CD
- Fluent Validation
- Serilog
- App Configuration
- Resilient Http Client


# Docker build command (from repo root folder)
docker build -t "<TAG>" -f .\CleanTemplate.Presentation\Dockerfile .

# Docker run command
docker run -p 8080:8080 -ti -e ASPNETCORE_ENVIRONMENT=<ENVIRONMENT-NAME><IMAGE>