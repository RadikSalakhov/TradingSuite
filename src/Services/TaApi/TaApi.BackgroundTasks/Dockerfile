#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/TaApi/TaApi.BackgroundTasks/TaApi.BackgroundTasks.csproj", "Services/TaApi/TaApi.BackgroundTasks/"]
RUN dotnet restore "Services/TaApi/TaApi.BackgroundTasks/TaApi.BackgroundTasks.csproj"
COPY . .
WORKDIR "/src/Services/TaApi/TaApi.BackgroundTasks"
RUN dotnet build "TaApi.BackgroundTasks.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TaApi.BackgroundTasks.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TaApi.BackgroundTasks.dll"]