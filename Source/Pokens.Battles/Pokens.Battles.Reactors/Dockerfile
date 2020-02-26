#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
# NuGet
COPY NuGet.config ./

COPY ["Pokens.Battles/Pokens.Battles.Reactors/Pokens.Battles.Reactors.csproj", "Pokens.Battles/Pokens.Battles.Reactors/"]
COPY ["Pokens.Battles/Pokens.Battles.Read.Domain/Pokens.Battles.Read.Domain.csproj", "Pokens.Battles/Pokens.Battles.Read.Domain/"]
COPY ["Pokens.Battles/Pokens.Battles.Domain/Pokens.Battles.Domain.csproj", "Pokens.Battles/Pokens.Battles.Domain/"]
COPY ["Pokens.Battles/Pokens.Battles.Resources/Pokens.Battles.Resources.csproj", "Pokens.Battles/Pokens.Battles.Resources/"]
RUN dotnet restore "Pokens.Battles/Pokens.Battles.Reactors/Pokens.Battles.Reactors.csproj"
COPY . .
WORKDIR "/src/Pokens.Battles/Pokens.Battles.Reactors"
RUN dotnet build "Pokens.Battles.Reactors.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Pokens.Battles.Reactors.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Pokens.Battles.Reactors.dll"]