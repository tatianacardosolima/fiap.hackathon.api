FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5011

ENV ASPNETCORE_URLS=http://+:5011
ENV ASPNETCORE_HTTP_PORTS=5011

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["src/Medicos/Fiap.Hackathon.Medicos.API/Fiap.Hackathon.Medicos.API.csproj", "src/Medicos/Fiap.Hackathon.Medicos.API/"]


RUN dotnet restore "src/Medicos/Fiap.Hackathon.Medicos.API/Fiap.Hackathon.Medicos.API.csproj"
COPY . .
WORKDIR "/src/src/Medicos/Fiap.Hackathon.Medicos.API"
RUN dotnet build "Fiap.Hackathon.Medicos.API.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "Fiap.Hackathon.Medicos.API.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Fiap.Hackathon.Medicos.API.dll"]