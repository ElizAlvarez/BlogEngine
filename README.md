# Blog Engine

Blog Engine is a solution that allows you to get, create, and update posts and comments. Depending on the user role there are different actions that can perform.
There are 3 roles:
- Public
- Writer
- Editor

Each role has some access to the application.

## Overview
The solution consists of 3 main parts:
1. BlogEngine: Is the Application. It contains the authentication (JWT), controllers and services.
2. BlogEngine.Core: Contains the interfaces, enumerations, and entities
3. BlogEngine.Data: Connect to the database using code first migration

This solution was implemented with the Onion Architecture. There is a core application in the center, and around there are the data and APIs. For more information about this architecture: [Onion Architecture](https://jeffreypalermo.com/2008/07/the-onion-architecture-part-1/)

## Unit Tests
To execute unit tests you can go to the test explorer in Visual Studio and click in the run green botton to run all the unit tests.

Note: For now, there are only some unit tests for post service and post controller, the idea is to create unit tests for each class from repository, service, and controller.

## Installation

1. Clone the repository to your local machine
2. Change the database connection string in BlogEngine.Data\BlogEngineDbContext class - Line 10
Example:

```c sharp
optionsBuilder.UseSqlServer(@"Server=localhost;Database=BlogTest;User ID=[User];Password=[Password];TrustServerCertificate=True;");
```

3. Build the solution. Open the Nuget package manager console. In the 'Default Project' option, select BlogEngine.Data
4. Execute 
```bash
PM> Add-Migration InitialReview -v
```
And then:
```bash
PM> Update-Database -v
```
5. After you execute the update-database command you can see the tables and records in the database.
6. So now, we can use the Postman collection to use the APIs

## Postman

1. Open the Postman collection 'BlogEngine.postman_colecction.json' and import the collection to your local Postman
2. You'll find three folders
- Post
- Comment
- Login
3. In the login folder you can choose any of these endpoints to obtain the token. After you send the endpoint you'll get the token, please copy that token.
4. Now, you can use any of the other endpoints (Post or Comment). 
```
Note: If you send a request with denied access, the status code of that request will be 403 Forbidden.
```
5. In one of the requests, go to the Authorization tab, select 'Bearer Token', and paste the token inside the input.
6. Also remember to change the payload of the request
7. Send the request

