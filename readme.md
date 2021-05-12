# ASP NET Core with VueJS (Quasar Framework) Blog Project

## Features
- Entity Framework Core – Code First 
- Response Wrappers
- Pagination,Search
- Net Core Identity with JWT Authentication,refresh token
- Role Based Authorization
- Database Seeding
- Custom Exception Handling Middleware
- Complete User Management  (Register / Generate Token / Forgot Password / Confirmation Mail)
- Logging (Serilog),Caching (Memory,Redis),Validation (Fluent Validation),Transaction,Exception,Performance with Aspects  (Autofac,Castle.DynamicProxy)

## How To Start Asp Net Core API

For asp net core, you must edit the appsettings.json file before typing these commands. 

```sh
dotnet ef migrations add CreateDatabase --project "DataAccess" --startup-project "WebAPI"
dotnet ef database update --project "DataAccess" --startup-project "WebAPI"
```
After these commands, a database will be created. 
Default Admin Account : 
```sh
Username : admin
Password : 159357456qW
```

## How To Start Quasar Project

Project requires [Node.js](https://nodejs.org/) , [Quasar Framework](https://quasar.dev) 

Quasar Admin Template : https://github.com/mayank091193/quasar-admin-crm

```sh
npm install
quasar dev
```


