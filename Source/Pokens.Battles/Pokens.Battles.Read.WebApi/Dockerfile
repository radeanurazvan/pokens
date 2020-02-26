#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
# NuGet
COPY NuGet.config ./

COPY ["Pokens.Battles/Pokens.Battles.Read.WebApi/Pokens.Battles.Read.WebApi.csproj", "Pokens.Battles/Pokens.Battles.Read.WebApi/"]
COPY ["Pokens.Battles/Pokens.Battles.Read.Domain/Pokens.Battles.Read.Domain.csproj", "Pokens.Battles/Pokens.Battles.Read.Domain/"]
RUN dotnet restore "Pokens.Battles/Pokens.Battles.Read.WebApi/Pokens.Battles.Read.WebApi.csproj"
COPY . .
WORKDIR "/src/Pokens.Battles/Pokens.Battles.Read.WebApi"
RUN dotnet build "Pokens.Battles.Read.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Pokens.Battles.Read.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Pokens.Battles.Read.WebApi.dll"]