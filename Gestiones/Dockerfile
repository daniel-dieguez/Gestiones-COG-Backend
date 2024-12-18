# Etapa de construcción
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copia el archivo .csproj y restaura las dependencias
COPY ["Gestiones.csproj", "./"]
RUN dotnet restore "Gestiones.csproj"

# Copia el resto del código fuente y compila
COPY . .
RUN dotnet build "Gestiones.csproj" -c $BUILD_CONFIGURATION -o /app/build 

# Etapa de publicación
FROM build AS publish
RUN dotnet publish "Gestiones.csproj" -c $BUILD_CONFIGURATION -o /app/publish

# Etapa final (imagen base de ASP.NET para ejecutar la aplicación)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Gestiones.dll"]
