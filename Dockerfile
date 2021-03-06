FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "MrRobot.API/MrRobot.API.csproj"
RUN dotnet build "MrRobot.API/MrRobot.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MrRobot.API/MrRobot.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MrRobot.API.dll"]