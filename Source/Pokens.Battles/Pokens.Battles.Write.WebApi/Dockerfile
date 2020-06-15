#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src

# NuGet
COPY NuGet.config ./

COPY ["Pokens.Battles/Pokens.Battles.Write.WebApi/Pokens.Battles.Write.WebApi.csproj", "Pokens.Battles/Pokens.Battles.Write.WebApi/"]
COPY ["Pokens.Battles/Pokens.Battles.Infrastructure/Pokens.Battles.Infrastructure.csproj", "Pokens.Battles/Pokens.Battles.Infrastructure/"]
COPY ["Pokens.Battles/Pokens.Battles.Business/Pokens.Battles.Business.csproj", "Pokens.Battles/Pokens.Battles.Business/"]
COPY ["Pokens.Battles/Pokens.Battles.Resources/Pokens.Battles.Resources.csproj", "Pokens.Battles/Pokens.Battles.Resources/"]
COPY ["Pokens.Battles/Pokens.Battles.Domain/Pokens.Battles.Domain.csproj", "Pokens.Battles/Pokens.Battles.Domain/"]
RUN dotnet restore "Pokens.Battles/Pokens.Battles.Write.WebApi/Pokens.Battles.Write.WebApi.csproj"
COPY . .
WORKDIR "/src/Pokens.Battles/Pokens.Battles.Write.WebApi"
RUN dotnet build "Pokens.Battles.Write.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Pokens.Battles.Write.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Pokens.Battles.Write.WebApi.dll"]