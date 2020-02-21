FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /app

COPY . ./
RUN dotnet publish IdentityIssuer.WebAPI -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2

WORKDIR /app
COPY --from=build /app/IdentityIssuer.WebAPI/out ./
ENTRYPOINT ["dotnet", "IdentityIssuer.WebAPI.dll"]