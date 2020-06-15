#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src

# NuGet
COPY NuGet.config ./

COPY ["Pokens.Trainers/Pokens.Trainers.Api/Pokens.Trainers.Api.csproj", "Pokens.Trainers/Pokens.Trainers.Api/"]
COPY ["Pokens.Trainers/Pokens.Trainers.Persistence.EntityFramework/Pokens.Trainers.Persistence.EntityFramework.csproj", "Pokens.Trainers/Pokens.Trainers.Persistence.EntityFramework/"]
COPY ["Pokens.Trainers/Pokens.Trainers.Domain/Pokens.Trainers.Domain.csproj", "Pokens.Trainers/Pokens.Trainers.Domain/"]
COPY ["Pokens.Trainers/Pokens.Trainers.Common/Pokens.Trainers.Common.csproj", "Pokens.Trainers/Pokens.Trainers.Common/"]
COPY ["Pokens.Trainers/Pokens.Trainers.Infrastructure/Pokens.Trainers.Infrastructure.csproj", "Pokens.Trainers/Pokens.Trainers.Infrastructure/"]
COPY ["Pokens.Trainers/Pokemons.Trainers.Business/Pokemons.Trainers.Business.csproj", "Pokens.Trainers/Pokemons.Trainers.Business/"]
RUN dotnet restore "Pokens.Trainers/Pokens.Trainers.Api/Pokens.Trainers.Api.csproj"
COPY . .
WORKDIR "/src/Pokens.Trainers/Pokens.Trainers.Api"
RUN dotnet build "Pokens.Trainers.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Pokens.Trainers.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Pokens.Trainers.Api.dll"]