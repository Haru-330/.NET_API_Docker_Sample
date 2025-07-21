FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5109

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["dotnet-api-sample.csproj", "."]
RUN dotnet restore "./dotnet-api-sample.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "dotnet-api-sample.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "dotnet-api-sample.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS="http://*:5109"
ENTRYPOINT ["dotnet", "dotnet-api-sample.dll"]