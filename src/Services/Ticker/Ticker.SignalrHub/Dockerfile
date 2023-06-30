#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 6090
EXPOSE 6091

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/Ticker/Ticker.SignalrHub/Ticker.SignalrHub.csproj", "Services/Ticker/Ticker.SignalrHub/"]
RUN dotnet restore "Services/Ticker/Ticker.SignalrHub/Ticker.SignalrHub.csproj"
COPY . .
WORKDIR "/src/Services/Ticker/Ticker.SignalrHub"
RUN dotnet build "Ticker.SignalrHub.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ticker.SignalrHub.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ticker.SignalrHub.dll"]