﻿FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["OptiCompare.csproj", "."]
RUN dotnet restore "./OptiCompare.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "OptiCompare.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OptiCompare.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OptiCompare.dll"]