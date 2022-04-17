#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Witchblades.Backend/Witchblades.Backend.Api/Witchblades.Backend.Api.csproj", "src/Witchblades.Backend/Witchblades.Backend.Api/"]
COPY ["src/Witchblades.Backend/Witchblades.Backend.Api.DataContracts/Witchblades.Backend.Api.DataContracts.csproj", "src/Witchblades.Backend/Witchblades.Backend.Api.DataContracts/"]
COPY ["src/Witchblades.Backend/Witchblades.Backend.Model/Witchblades.Backend.Model.csproj", "src/Witchblades.Backend/Witchblades.Backend.Model/"]
COPY ["src/Witchblades.Backend/Witchblades.Backend.Data/Witchblades.Backend.Data.csproj", "src/Witchblades.Backend/Witchblades.Backend.Data/"]
RUN dotnet restore "src/Witchblades.Backend/Witchblades.Backend.Api/Witchblades.Backend.Api.csproj"
COPY . .
WORKDIR "/src/src/Witchblades.Backend/Witchblades.Backend.Api"
RUN dotnet build "Witchblades.Backend.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Witchblades.Backend.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Witchblades.Backend.Api.dll"]