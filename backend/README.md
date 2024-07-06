## Project Details
* This project uses ``PostgreSQL`` for it's backend

* It uses "named connection strings" to connect to the database.

To configure it, open Package Manager Console (from View -> Other Windows -> Package Manager Console) and type:

```
dotnet user-secrets init
dotnet user-secrets set ConnectionStrings:bank_db "{connection string should come here}"
```

* Put Connection String without curly braces: ``{`` and ``}``

To give an example of Connection String for PostgreSQL:
```
Host=localhost; Database=db_name; Username=postgres; Password=123
```

* The SQL commands required to create the database can be accessed from the [database-scripts folder](../database-scripts).
