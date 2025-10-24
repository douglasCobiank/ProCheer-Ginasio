# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia apenas o csproj da API e restaura dependências
COPY ["Ginasio.Api/Ginasio.Api.csproj", "Ginasio.Api/"]
COPY ["Ginasio.Core/Ginasio.Core.csproj", "Ginasio.Core/"]
COPY ["Ginasio.Infrastructure/Ginasio.Infrastructure.csproj", "Ginasio.Infrastructure/"]

RUN dotnet restore "Ginasio.Api/Ginasio.Api.csproj"

# Copia o resto dos arquivos e faz o build
COPY . .
WORKDIR "/Ginasio.Api"
RUN dotnet publish "Ginasio.Api.csproj" -c Release -o /app

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app .
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "Ginasio.Api.dll"]