#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src

# NuGet
COPY NuGet.config ./

COPY ["Pokens.Pokedex/Pokens.Pokedex.Office/Pokens.Pokedex.Office.csproj", "Pokens.Pokedex/Pokens.Pokedex.Office/"]
COPY ["Pokens.Pokedex/Pokens.Pokedex.Business/Pokens.Pokedex.Business.csproj", "Pokens.Pokedex/Pokens.Pokedex.Business/"]
COPY ["Pokens.Pokedex/Pokens.Pokedex.Domain/Pokens.Pokedex.Domain.csproj", "Pokens.Pokedex/Pokens.Pokedex.Domain/"]
COPY ["Pokens.Pokedex/Pokens.Pokedex.Infrastructure/Pokens.Pokedex.Infrastructure.csproj", "Pokens.Pokedex/Pokens.Pokedex.Infrastructure/"]
RUN dotnet restore "Pokens.Pokedex/Pokens.Pokedex.Office/Pokens.Pokedex.Office.csproj"
COPY . .
WORKDIR "/src/Pokens.Pokedex/Pokens.Pokedex.Office"
RUN dotnet build "Pokens.Pokedex.Office.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Pokens.Pokedex.Office.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Pokens.Pokedex.Office.dll"]