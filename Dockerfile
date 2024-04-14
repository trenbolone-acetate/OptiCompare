FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 44335
COPY ./localhost.pfx /https/

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["OptiCompare.csproj", "./"]
RUN dotnet restore "OptiCompare.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "OptiCompare.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "OptiCompare.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OptiCompare.dll", "--urls", "https://0.0.0.0:44335"]