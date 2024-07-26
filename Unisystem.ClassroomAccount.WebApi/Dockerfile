#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Unisystem.ClassroomAccount.WebApi/Unisystem.ClassroomAccount.WebApi.csproj", "Unisystem.ClassroomAccount.WebApi/"]
RUN dotnet restore "./Unisystem.ClassroomAccount.WebApi/Unisystem.ClassroomAccount.WebApi.csproj"
COPY . .
WORKDIR "/src/Unisystem.ClassroomAccount.WebApi"
RUN dotnet build "./Unisystem.ClassroomAccount.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Unisystem.ClassroomAccount.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Unisystem.ClassroomAccount.WebApi.dll"]