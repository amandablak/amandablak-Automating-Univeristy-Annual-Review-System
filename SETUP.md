# Faculty APR Setup Guide
## Front End
Follow steps in README. Make sure you install all required items in the tech stack section.
## Back End
Create appsettings.json file in FacultyAPR.API folder. 

`cd FacultyAPR.API`

`touch appsettings.json;`

`echo {
    "AzureAd": {
      "Domain": "redhawks.onmicrosoft.com",
      "ClientId": "27226534-0bd7-4dba-b895-040efd8f8602",
      "ClientSecret": "bsFoo5RoH_8x-aBPsg31gv.uvuO5_bvJ0J",
      "Instance": "https://login.microsoftonline.com/",
      "TenantId": "bc10e052-b01c-4849-9967-ee7ec74fc9d8"
    },
    "DownstreamAPI": {
      "Scopes": "User.Read",
      "BaseUrl": "https://graph.microsoft.com/v1.0/"
    },
    "https_port": 44351,
    "Logging": {
      "LogLevel": {
        "Default": "Information",
        "Microsoft": "Warning",
        "Microsoft.Hosting.Lifetime": "Information"
      }
    },
    "AllowedHosts": "*",
    "SQLForm" :
    {
      "ConnectionString": "host=127.0.0.1; port=3306;User ID=yourid;Password=yourpassword;Database=test",
      "Name": "APRTEST"
    },
    "SQLUser" :
    {
      "ConnectionString": "host=127.0.0.1; port=3306;User ID=yourid;Password=yourpassword;Database=test",
      "Name": "APRTEST"
    }
  } > appsettings.json`

## Database
setup mysqlserver

execute `lib/FacultyAPR.Models/BuisnessObjects/Database/SetupDatabase.sql`

You must be an admin to create users, forms, etc. Do this by adding yourself in the users table

```sql
insert into UserInfo (UserId, FirstName, LastName, EmailAddress, UserType)
values ("ac6655ca-b2ef-4a50-91c7-ee34ca566c44", "Eric", "Larson", "elarson@seattleu.edu", "Admin"),
("bc6655ca-b2ef-4a50-91c7-ee34ca566c44", "Eric", "Larson", "elarson@seattleu.edu", "FacultyChair"),
("cc6655ca-b2ef-4a50-91c7-ee34ca566c44", "Eric", "Larson", "elarson@seattleu.edu", "Faculty")

insert into Department (DepartmentId, DepartmentName, DepartmentChair) VALUES
(1,	"ComputerScience",	"bc6655ca-b2ef-4a50-91c7-ee34ca566c44")

insert into Faculty (UserId, FacultyRank, DepartmentId) VALUES
("cc6655ca-b2ef-4a50-91c7-ee34ca566c44",	"Instructor",	1),
("bc6655ca-b2ef-4a50-91c7-ee34ca566c44",	"SeniorInstructor",	1)
```

Notes: For userId generate a random Guid/UUID

## Create Form Schema
Grab bearer token from frontend
Any request to the backend will have a header called Authorization. Use this value as the token. For example.

`Authorization
Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6Im5PbzNaRHJPRFhFSzFqS1doWHNsSFJfS1hFZyIsImtpZCI6Im5PbzNaRHJPRFhFSzFqS1doWHNsSFJfS1hFZyJ9.eyJhdWQiOiJhcGk6Ly8yNzIyNjUzNC0wYmQ3LTRkYmEtYjg5NS0wNDBlZmQ4Zjg2MDIiLCJpc3MiOiJodHRwczovL3N0cy53aW5kb3dzLm5ldC9iYzEwZTA1Mi1iMDFjLTQ4NDktOTk2Ny1lZTdlYzc0ZmM5ZDgvIiwiaWF0IjoxNjIyMDQ5MDE4LCJuYmYiOjE2MjIwNDkwMTgsImV4cCI6MTYyMjA1MjkxOCwiYWNyIjoiMSIsImFpbyI6IkFUUUF5LzhUQUFBQWZSOEpqcitFU0dua0lXenJ1UTRwR2YvbGlYaElNMXFTemJBaC9LNllEMW1DeDFuQWZnQ284Mk9uMEp2eHZhQ2siLCJhbXIiOlsicHdk…OS05OTY3LWVlN2VjNzRmYzlkOCIsInVuaXF1ZV9uYW1lIjoid2Fzc3FAc2VhdHRsZXUuZWR1IiwidXBuIjoid2Fzc3FAc2VhdHRsZXUuZWR1IiwidXRpIjoiM1BkeU1ubUhna1dQRTZzc0RJWDRBUSIsInZlciI6IjEuMCJ9.VbQch_sSR77HPAN4m345EOQgsKv6RQTBMbYpc8U6k_sb7US0qTSlSumLAdL60vOIGWGP3xt5oCmXF_tsQzsXPjiSNV_7FXErMp75wU4gVbERiNpoo-1CMM2zaELtknLNnQIM_zhhwWPSqbc5wJsVFfT-Pv6bqduvkDvjG1CM5RRd_UqQgYPhN5Gwsauemu0-CaiGIoARaEVTzHTlGbVTtE_7xHZa8blbEUOYlDs2ZlFC96jSEGBybC7LaUNjEEByo9uzWbrHboMOhuw6LgD_dmAJL3DNPxjWgO4nWkAddY3Mr0pBXNS6wXuRgjxfhS0cWvYnwy7d-IxriSyNzodPIA
`

Use `lib/FacultyAPR.Models/BuisnessObjects/Database/2021ProfessorStructure.json`
to create a schema. 

POST to `https://{domain}/FormStructure/{year}/{rank}`

Example `https://localhost:5001/FormStructure/2021/Professor`

Other routes are in controller files.

