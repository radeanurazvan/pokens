#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src

# NuGet
COPY NuGet.config ./

COPY ["Pokens.Training/Pokens.Training.Api/Pokens.Training.Api.csproj", "Pokens.Training/Pokens.Training.Api/"]
COPY ["Pokens.Training/Pokens.Training.Infrastructure/Pokens.Training.Infrastructure.csproj", "Pokens.Training/Pokens.Training.Infrastructure/"]
COPY ["Pokens.Training/Pokens.Training.Domain/Pokens.Training.Domain.csproj", "Pokens.Training/Pokens.Training.Domain/"]
COPY ["Pokens.Training/Pokens.Training.Resources/Pokens.Training.Resources.csproj", "Pokens.Training/Pokens.Training.Resources/"]
COPY ["Pokens.Training/Pokens.Training.Business/Pokens.Training.Business.csproj", "Pokens.Training/Pokens.Training.Business/"]
RUN dotnet restore "Pokens.Training/Pokens.Training.Api/Pokens.Training.Api.csproj"
COPY . .
WORKDIR "/src/Pokens.Training/Pokens.Training.Api"
RUN dotnet build "Pokens.Training.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Pokens.Training.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Pokens.Training.Api.dll"]