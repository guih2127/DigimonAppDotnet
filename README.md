# Digimon App
This project is an API made with .NET Core 6, with the goal to manipulate data of Digimons

## How to run this code?
For now, you can run the project only by having on your machine the .NET Core SDK installed and an instance of SQL Server (It can be Docker or locally).

After that, you need to create the a new database in the instance that you want to use. After that, you need to get your connection string and change the one in DigimonApp/Program.cs. It should look like that:

```
Server=localhost,1433;Database=yourdatabase;User ID =youruser;Password=yourpassword$
```

For the last, you need to enter the DigimonApp folter and apply the migrations (Create the tables and insert some data), using the following command, if you are using Visual Studio:

```
Update-Database
```

Or, using the .NET Core CLI

```
dotnet ef database update
```

After that, you can test the API locally and see it's own documentation, using the folloowing URL:

```
localhost:5110/swagger/index.html
```

The number of the port depends of how you are running this project.

## Running the tests
Tests were created for the Services of this API, you can run then using Visual Studio or, if you are using .NET Core CLI

```
dotnet test
```
