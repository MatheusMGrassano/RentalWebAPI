#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

ENV ASPNETCORE_ENVIRONMENT=Development

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Rental_WebApi/Rental.API.csproj", "Rental_WebApi/"]
COPY ["Rental.BLL/Rental.BLL.csproj", "Rental.BLL/"]
COPY ["Rental.DAL/Rental.DAL.csproj", "Rental.DAL/"]
RUN dotnet restore "Rental_WebApi/Rental.API.csproj"
COPY . .
WORKDIR "/src/Rental_WebApi"
RUN dotnet build "Rental.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Rental.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Rental.API.dll"]