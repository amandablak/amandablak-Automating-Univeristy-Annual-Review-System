# Lib
This folder contains the many custom libraries that we use for the dotnet server (FacultyAPR.API).

## FacultyAPR.Models
Contains the various interfaces, data classes and mock classes used in the server and testing. Notable files and folders include:
- lib\FacultyAPR.Models\BuisnessObjects\Database\SetupDatabase.sql
  - this file sets up the SQL database and initializes some test values for some tables.
- lib\FacultyAPR.Models\BuisnessObjects\Database\2021ProfessorStructure.json
  - this very important file sets the form structure for a specific faculty rank and year. You can send this file as part of the body of a HTTP POST request to {server address}/FormStructure/{year}/{rank}. An example may be `http://localhost:5000/FormStructure/2021/Professor`
- lib\FacultyAPR.Models\FacultyAPR.Models\BuisnessObjects\Form
- lib\FacultyAPR.Models\FacultyAPR.Models\Users
- lib\FacultyAPR.Models\FacultyAPR.Models.Tests

## FacultyAPR.Storage
Interfaces and exceptions used for the sqlStores.

## FacultyAPR.Storage.Blob
Interfaces and unit tests for blob storage.

## FacultyAPR.Storage.Blob.Azure
Unimplemented Azure Blob Store.

## FacultyAPR.Storage.Sql
Implementation of the SQL form/user stores. Notable files include:
- lib\FacultyAPR.Storage.Sql\SqlFormStore.cs
- lib\FacultyAPR.Storage.Sql\sqlUserStore.cs
both of which implement safe SQL queries of the relevant controller methods.

## FacultyAPR.Storage.Sql.Integration.Tests
Integration tests for the SQL stores defined above.

## FacultyAPR.Testing.Utilities
Utility classes to aid in integration tests.