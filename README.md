# IdentityIssuer ![Build status](https://travis-ci.com/Saaka/IdentityIssuer.svg?branch=master)

IdentityIssuer lets you manage user accounts for multiple application in one place.
Create Tenants with independent users. Each of them enables multiple registration options like classic credentials or Social logins: Google, Facebook (more comming soon).

## Docker 
#### WebAPI
Run commands to build and run Web API in docker.

`docker build -t identityissuer -f ./IdentityIssuer.WebAPI/Dockerfile .`

Change `ASPNETCORE_ENVIRONMENT` value depending on your needs.

`docker run -p 8080:80 -e ASPNETCORE_ENVIRONMENT=Development identityissuer`