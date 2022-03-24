#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["File.Server.Api/File.Server.Api.csproj", "File.Server.Api/"]
RUN dotnet restore "File.Server.Api/File.Server.Api.csproj"
COPY . .
WORKDIR "/src/File.Server.Api"
RUN dotnet build "File.Server.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "File.Server.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "File.Server.Api.dll"]