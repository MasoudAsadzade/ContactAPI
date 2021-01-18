 # ContactAPI on CleanArchitecture
ContactAPI on Clean Architecture and CQRS patern for ASP.NET Core 5.0. Built with Onion/Hexagonal Architecture.
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
* Run the following commands to install the required packages, run, and start the application on your local as your will.
``` 
Cd to {YourPath}\ContactAPI\ContactAPI.Web
dotnet restore
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

Please run following commands to update your database from existing migrations and then run the project:
```
cd to {ContactAPI}.Infrastructure
 
 dotnet ef database update --startup-project ../ContactAPI.Api/ContactAPI.Api.csproj -c "IdentityContext"

 dotnet ef database update --startup-project ../ContactAPI.Api/ContactAPI.Api.csproj -c "ApplicationDbContext"
```
# Test Instruction

1. Add two skills by AddSkill
2. Add user by Identity -> register 
3. Use your email address and password to Generates a JSON Web Token by Identity -> token.

4. Copy the following JWT token to use for authentication. and id as UserId to add Contact.
(Contact table is exposing some subject-to-change attribute of users. like FirstName, Address,..)

![alt text](ContactAPI.Api\Figs\Token.png)

5. Insert JWT token ->  Bearer {Token}

![alt text](ContactAPI.Api\Figs\JWT.png)

6. Add Contact by UserId

7. You can Edit your Contact By ContactId returned "Add Contact"

8. Add Skill for your Contact using UserId and SkillId by ContactSkill -> Add User's Skills
### Customized Database Migrations

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



