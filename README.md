# PK-Server

## Requirements
.NET 5.0 https://dotnet.microsoft.com/en-us/download/dotnet/5.0
A copy of the .NET runtime and SDK is needed.  Version 6.0 is also usable with the application.  In order to use version 6.0, all projects need to be migrated.

MySQL https://www.mysql.com/downloads/
For development of the application, MySQL community was used.  For usage at PK sound, it is most likely that MySQL enterprise will be needed.  The core download needed is MySQL server.  MySQL workbench is also recommended.

MapQuest Api Key https://developer.mapquest.com/
A key for the MapQuest API is needed.  The application can work without MapQuest but notifications for rentals arriving at venues will be disabled.

## Setup
### AppSettings
Before running, Web-Api/appsettings_example.json needs to be copied to appsettings.json or appsettings.Development.json.  Then update appsettings.json or appsettings.Development.json with user secret information.
1) Update “ApplicationMySQLDataBase” to contain your MySQL server ip, database name, user id and password.
2) Update the mapquest api key to the Consumer Key given to your mapquest account (Consumer Secret is not needed).    
3) Update the “BaseURL” to the ip and port of the deployed web application.  This needs to be correct for the CORS policy.

### MySQL
To use the application, the seed found at Infrastructure\Persistence\Seed\seed.sql needs to be run to build the tables.

## Usage
From the root folder run the following:
`dotnet run --project Web-api`
The swagger api should be running on port 5001 if https is used and port 5000 if unused.
