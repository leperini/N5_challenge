#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["N5_challenge/N5_challenge.csproj", "N5_challenge/"]
RUN dotnet restore "N5_challenge/N5_challenge.csproj"
COPY . .
WORKDIR "/src/N5_challenge"
RUN dotnet build "N5_challenge.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "N5_challenge.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "N5_challenge.dll"]