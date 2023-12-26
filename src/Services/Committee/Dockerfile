FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /source
COPY . .
# # copy csproj and restore as distinct layers

RUN dotnet restore API/News.API.csproj

RUN dotnet restore Core/News.Application/News.Application.csproj

RUN dotnet restore Core/News.Domain/News.Domain.csproj

RUN dotnet restore Core/News.Infrastructure/News.Infrastructure.csproj

# # copy everything else and build app
WORKDIR /source/API
# RUN dotnet build -c Release -o /app/build

# FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app

# # final stage/image
COPY --from=build /app ./

WORKDIR /app/publish
# # Start the application
ENTRYPOINT ["dotnet", "News.API.dll"]