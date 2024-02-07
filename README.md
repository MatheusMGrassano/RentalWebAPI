# Rental Web API

### Sections

[Description](#description)

[Features](#features)

[Tools required](#tools-required)

[Running the application (source code)](#running-the-application-source-code)

[Running the application (Docker)](#running-the-application-docker)

[Testing app endpoints](#testing-app-endpoints)

[Libraries and tools](#libraries-and-tools)


## Description
🇬🇧
This repository contains a Web API project built with .NET 7 with the goal of implementing functionalities for a motorcycles rental app, including logins with secure password storage, JWT Bearer authentication, registration, rental and return of motorcycles.


🇧🇷
Este repositório contém um projeto Web API construído em .NET 7 com o objetivo de implementar funcionalidades de um aplicativo de aluguel de motocicletas, incluindo logins com armazenamento seguro de senhas, autenticação JWT Bearer, cadastro, aluguel e devolução de motocicletas.

## Features

### Admin
| Method | Route                       | JSON Body                          | Action                          |
| ------ | --------------------------- | ---------------------------------- | ------------------------------- |
| POST   | /api/Admin/login            | email + password                   | Generate an access token         |
| POST   | /api/Admin/create           | email + password                   | Create an admin user             |
| PUT    | /api/Admin/passwordupdate   | email + password + newPassword     | Update an admin's password       |
| DELETE | /api/Admin/delete           | email + password                   | Remove an admin user             |

### Driver
| Method | Route                       | JSON Body                          | Action                          |
| ------ | --------------------------- | ---------------------------------- | ------------------------------- |
| POST   | /api/Driver/login           | email + password                   | Generate a access token         |
| POST   | /api/Driver/create          | email + password + driver's info   | Create a driver user            |
| POST   | /api/Driver/rent            | email + password + rentDays        | Rents an available motorcycle   |
| POST   | /api/Driver/return          | email + password + plate + endDate | Return a rented motorcycle      |

### Motorcycle
| Method | Route                            | JSON Body                       | Action                          |
| ------ | -------------------------------- | ------------------------------- | ------------------------------- |
| GET    | /api/Motorcycle?offset=5&limit=5 |                                 | Get the motorcycles list        |
| GET    | /api/Motorcycle/id/{id}          |                                 | Get a motorcycle by id          |
| GET    | /api/Motorcycle/plate/{plate}    |                                 | Get a motorcycle by plate       |
| POST   | /api/Motorcycle/create           | manufactureYear + model + plate | Register a motorcycle           |
| PUT    | /api/Motorcycle/update           | id + newPlate                   | Update a motorcycle's plate     |
| DELETE | /api/Motorcycle/delete/{id}      |                                 | Remove a motorcycle             |

### Request body example
```json
{
  "email": "string",
  "password": "string"
}
```

### Response body example
```json
{
  "data": {
    "id": 0,
    "manufactureYear": 0,
    "model": "string",
    "plate": "string",
    "available": true
  },
  "statusCode": 100,
  "message": "string"
}
```


## Tools required

To run the application, you must have these tools installed:

- [.NET 7.0 SDK](https://dotnet.microsoft.com/pt-br/download/dotnet/7.0)
- [PostgreSQL](https://www.postgresql.org/download/)

Or
- [Docker](https://www.docker.com/)

## Running the application (source code)

Download or clone the project from: [https://github.com/MatheusMGrassano/RentalWebAPI.git](https://github.com/MatheusMGrassano/RentalWebAPI.git)

### Connection String

Make sure to change the database connection credentials in the appsettings.json file to match your setting.

### Running the application

At your terminal, access the project folder Rental_WebApi and type the following command:

```bash
dotnet watch run
```

A new browser tab will open with the Swagger UI showing all the endpoints available.


## Running the application (Docker)

### Create docker-compose.yml
Create a docker-compose.yml file with following code inside
```yml
version: '3.8'

services:
  rental-api:
    image: matheusgrassano/rental-api:latest
    ports:
      - "5000:80"
    depends_on:
      - postgres
  postgres:
    image: postgres:15.5-bullseye
    restart: always
    environment:
      POSTGRES_USER: root
      POSTGRES_PASSWORD: root
      POSTGRES_DB: RentalDb
    ports:
      - "5432:5432"
    volumes:
      - pgdata:/var/lib/postgresql/data

volumes:
  pgdata:
```
*Warning: if you make changes to the docker-compose.yml don't forget to apply those changes to the app connection string at appsettings.json*

### Run the docker-compose.yml file
At your terminal, go to the folder where the docker-compose.yml is and run the following command:
```bash
docker compose up -d
```
Now, both containers (rental-api and postgres) should be running

### Test the app with Swagger UI
On your browser, go to [localhost:5000/swagger/index.html](localhost:5000/swagger/index.html) and the Swagger UI should appear with all the endpoints available.

## Testing app endpoints
In order to test all endpoints, first you must create a user (whether Admin/create or Driver/create). After creating the user, use the login endpoint to generate your access token.

Enter the new token in the JWT Authorization header using the Bearer scheme.
Enter 'Bearer' [space] and then your token in the header.

Example: "Bearer 12345abcdef"

Now, you are able to access all endpoints using the access token.

## Libraries and tools

- [Entity Framework Core](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore/)
- [Entity Framework Core Tools](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Tools)
- [Entity Framework Core Design](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Design)
- [Entity Framework Core PostgreSQL](https://www.nuget.org/packages/Npgsql.EntityFrameworkCore.PostgreSQL)
- [ASP.NET Core JWT Bearer Authentication](https://www.nuget.org/packages/Microsoft.AspNetCore.Authentication.JwtBearer/)
- [PostgreSQL](https://www.postgresql.org/)
- [Swagger UI](https://swagger.io/tools/swagger-ui/)
- [Docker](https://www.docker.com/)
