# Etapa de construcción
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

COPY *.csproj ./
RUN dotnet restore

COPY . ./

RUN dotnet publish -c Release -o out

# Etapa de ejecución
FROM mcr.microsoft.com/dotnet/sdk:6.0
WORKDIR /app
COPY --from=build /app/out ./

ENV ASPNETCORE_URLS=https://+:7203;http://+:5155

EXPOSE 80
EXPOSE 7203

ENTRYPOINT ["dotnet", "N5.dll"]
