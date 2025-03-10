## Project Details
* This project uses ``PostgreSQL`` for it's backend

* There are some secure environment variables used in the project.

To configure them, open Package Manager Console (from View -> Other Windows -> Package Manager Console) and type:

:exclamation: All data is inserted without curly braces: ``{`` and ``}``
```
dotnet user-secrets init
dotnet user-secrets set Jwt:Key "{64 char random key}"
dotnet user-secrets set Jwt:Issuer "{IP and port where the server is running}"
dotnet user-secrets set Jwt:Audience "{target audience address that will consume this API}"
dotnet user-secrets set ConnectionStrings:bank_db "{connection string should come here}"
dotnet user-secrets set Smpt:Port "{64 char random key}"
dotnet user-secrets set Smpt:Server "{64 char random key}"
dotnet user-secrets set Smpt:Username "{64 char random key}"
dotnet user-secrets set Smpt:Password "{64 char random key}"
```

To give an example of Connection String for PostgreSQL:
```
Host=localhost; Database=db_name; Username=postgres; Password=123
```

* To generate the database, in Package Manager Console, type:
```
Update-Database 0
Update-Database
```
