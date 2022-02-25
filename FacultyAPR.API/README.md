# FacultyAPR.API
This is a minimal backend server, focused on providing APIs for the frontend to hit to query the SWL database. Each API is defined by a controller method in `/Controllers`. Most controllers make a call to the SQL database using either an IUserStore or IFormStore. Those types are implemented in `/lib/FacultyAPR.Storage.Sql/`


!!IMPORTANT!!

In order for the server successfully to connect to a SQL database and allow clients to interact with it, you need to create an `appsettings.json` in this folder. An example appsettings may look like this:
```json
{
    "AllowedHosts": "*",
    "SQLForm" :
    {
      "ConnectionString": "host=127.0.0.1; port=3306;User ID=root;Password=example;Database=test",
      "Name": "TestDB"
    },
    "SQLUser" :
    {
      "ConnectionString": "host=127.0.0.1; port=3306;User ID=root;Password=example;Database=test",
      "Name": "TestDB"
    }
}
```
More documentation can be found [here](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-3.1)