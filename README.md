# Social Networking Kata
Excercise that aims to implement a console-based social networking application (similar to Twitter) satisfying the scenarios below.
(See user stories and Task in the public page of my Azure DevOps: https://dev.azure.com/lunard/Social%20Networking%20Kata/_workitems/recentlyupdated/)

# Database
The Project Database\SocialNetworkingKataDatabase.csprj can be used to created the needed SQL Database on the default .\SQLEXPRESS 
instance (if neeed change the connection in the scmp file).
This project can be used in an Azure DevOps pipeline to validate/migrate the database (using the dacpac file).
Please launch the post deployment script file 'UserPostDeployment.sql' after creating the DB.

# Exercise assumptions 
## User creation
The user creation is not specified in the User Stories. 
Assumption: only the Users insert using the post deployment script can be used at runtime

# What is missing
I focused not on the ConsoleApplication itself, but on the Services/Infrastructure/Database part of the architecture
I followed the DDD design, to implement a Clean Architecture

## What is missing in the Program class
Generic Host: would be really better to implement the console application with a GenericHost, using the HostBuilder
Logging ... Serilog with its Skins !!
Dependency Injection using Autofac

## What is missing in the User.Service project
Could be useful to decuple the code execution using the mediator pattern (eg via MediatR)