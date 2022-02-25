# CS-21.16

Tech Stack:
* dotnet 3.1 LTS
* MySQL 8.0
* Nodejs 14.15 LTS

# How to Navigate this Project
Documentation can be located under `docs`.
Diagrams can be located under `docs/diagrams`.
A high-level architecture overview can be found in `docs/Design Document.docx`.

# Project Organization
## FacultyAPR.API
The backend server, written in C#.

To start the API server in Linux/MacOS run:

`dotnet run ./FacultyAPR.API/FacultyAPR.API.csproj`

To start the API server in Windows run:

`dotnet run --project FacultyAPR.API\FacultyAPR.API.csproj`

## FacultyAPR.Client
The frontend is written in JavaScript, using Node.js, ReactJS and TypeScript.

To start the frontend server run:

```sh
cd FacultyAPR.Client
npm i
npm start
```

## Lib
Models and important dotnet project libraries stored here. Notable files include:
+ lib\FacultyAPR.Models\BuisnessObjects\Database\SetupDatabase.sql
+ lib\FacultyAPR.Storage.Sql\SqlFormStore.cs
+ lib\FacultyAPR.Models\BuisnessObjects\Database\2021ProfessorStructure.json

## Docker
While we do not explicitly use Docker to run the server or database, you could use docker-compose instantiate the database. An example is given here:
```yml
version: '3'
services:
  db:
    container_name: apr-mysql
    image: mysql # or mariadb
    restart: always
    ports:
      - 3306:3306
    volumes:
      # - ./CS-21.16/lib/FacultyAPR.Models/BuisnessObjects/Database:/docker-entrypoint-initdb.d
      - apr-mysql-data:/var/lib/mysql
    environment:
      MYSQL_ROOT_PASSWORD: example
      MYSQL_DATABASE: test

  # to interface with the sql database
  admin:
    image: adminer
    restart: always
    ports:
      - "8080:8080"

volumes:
  apr-mysql-data:
```
Be warned, we experienced an issue where our SetupDatabase.sql would generate a "index is too long" for some tables using adminer, but produced no issues using MySQL Workbench.