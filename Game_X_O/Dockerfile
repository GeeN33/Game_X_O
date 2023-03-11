#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Game_X_O/Game_X_O.csproj", "Game_X_O/"]
RUN dotnet restore "Game_X_O/Game_X_O.csproj"
COPY . .
WORKDIR "/src/Game_X_O"
RUN dotnet build "Game_X_O.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Game_X_O.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Game_X_O.dll"]