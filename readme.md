# ASP NET Core with VueJS 2 (Quasar Framework) Blog Project

![Blog Optimized](https://user-images.githubusercontent.com/32078573/137368016-2cf5135a-f811-4aac-93a8-3c7197ff7d99.gif)
## Features
- Entity Framework Core – Code First 
- Response Wrappers
- Pagination,Search
- Net Core Identity with JWT Authentication,Refresh Token
- Role Based Authorization
- Database Seeding
- Custom Exception Handling Middleware
- Complete User Management  (Register / Generate Token / Forgot Password / Confirmation Mail)
- Logging (Serilog),Caching (Memory,Redis),Validation (Fluent Validation),Transaction,Exception,Performance with Aspects (Autofac,Castle.DynamicProxy)

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

Project requires [Quasar Framework](https://quasar.dev) 

You must edit  mysitequasar\src\store\siteinformation\state.js 
apiurl and defaultphoto url 

```sh
npm install
quasar dev
```



