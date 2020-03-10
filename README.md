# IdentityIssuer ![Build status](https://travis-ci.com/Saaka/IdentityIssuer.svg?branch=master)

IdentityIssuer lets you manage user accounts for multiple applications in one place.

You can create Tenants with independent users. Each of them enables multiple registration options like classic credentials or social logins like Google and Facebook with more coming soon.

## Appsettings

`ConnectionStrings` section is most important as you need to provide valid MSSQL Server address.
`Issuer` in `Auth` section is adds an extra validation for our tokens. 
`Google` and `Facebook` validation inputs are preset and should not be changed most of the time.

* `ConnectionStrings`
    * `AppConnectionString` - MSSQL database address
    
* `Auth`
    * `Issuer` - The issuer name. It is validated during JWT Token authorization process.

* `Google`
    * `ValidationEndpoint` - API Endpoint for Google token validation. 

* `Facebook`
    * `ValidationEndpoint` - API Endpoint for Facebook token validation. 
    
## Docker 
#### WebAPI
Run commands to build and run Web API in docker (from `Src` directory).

`docker build -t identityissuer -f ./IdentityIssuer.WebAPI/Dockerfile .`

Change `ASPNETCORE_ENVIRONMENT` value depending on your needs.

`docker run -p 8080:80 -e ASPNETCORE_ENVIRONMENT=Development identityissuer`

You can add environmental variable to override connection string from configuration files:

`-e ASPNETCORE_ConnectionStrings_AppConnectionString="AppConnectionString": "Server=localhost,1433;Database=IdentityIssuer;Integrated Security=false;User id=sa;Password=admin;MultipleActiveResultSets=true" `
