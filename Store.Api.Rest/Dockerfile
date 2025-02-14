#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Store.Api.Rest/Store.Api.Rest.csproj", "Store.Api.Rest/"]
RUN dotnet restore "Store.Api.Rest/Store.Api.Rest.csproj"
COPY . .
WORKDIR "/src/Store.Api.Rest"
RUN dotnet build "Store.Api.Rest.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Store.Api.Rest.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Store.Api.Rest.dll"]