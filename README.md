 # ContactAPI on CleanArchitecture
ContactAPI on Clean Architecture for ASP.NET Core 5.0. Built with Onion/Hexagonal Architecture.
## Technologies
* ASP.NET Core 5.0 WebAPI (JWT and Swagger)
* Entity Framework Core 5.0
* MediatR
* Mapster
* FluentValidation

## Getting Started
* Get the current *main* version: 
``` 
git clone https://github.com/MasoudAsadzade/ContactAPI
``` 
* Run the following commands to install the required packages, run database migrations, and start the application on your local as your will.
``` 
Cd to {YourPath}\ContactAPI\ContactAPI.Web
dotnet restore
dotnet ef database update
dotnet build
dotnet run
``` 


Open your web browser and browse to https://localhost:5001/swagger/index.html to see Swagger UI

## Database Configuration

The solution is configured to use an in-memory database by default.
If you would like to use SQL Server, you will need to update **WebApi/appsettings.json** as follows:

```json
  "UseInMemoryDatabase": false,
```

Verify that the **ApplicationConnection** connection string along with **IdentityConnection** within **appsettings.json** points to a valid SQL Server instance. 

When you run the application the database will be automatically created (if necessary) and the latest migrations will be applied.

### Database Migrations

Please run following commands to build and run your migrations
```
cd to {ContactAPI}.Infrastructure
```
```
 dotnet ef migrations add InitialDB --startup-project ../ContactAPI.Api/ContactAPI.Api.csproj -c "IdentityContext"
 ```
 ```
 dotnet ef database update --startup-project ../ContactAPI.Api/ContactAPI.Api.csproj -c "IdentityContext"
```
 ```
 dotnet ef migrations add InitialDB --startup-project ../ContactAPI.Api/ContactAPI.Api.csproj -c "ApplicationDbContext"
```
```
 dotnet ef database update --startup-project ../ContactAPI.Api/ContactAPI.Api.csproj -c "ApplicationDbContext"
```



