FROM mcr.microsoft.com/dotnet/runtime:7.0-alpine AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine AS build
WORKDIR /src
COPY ["TCBApp/TCBApp.csproj", "TCBApp/"]
COPY ["TCBApp.Core/TCBApp.Core.csproj", "TCBApp.Core/"]
RUN dotnet restore "TCBApp/TCBApp.csproj"
COPY . .
WORKDIR "/src/TCBApp"
RUN dotnet build "TCBApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TCBApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TCBApp.dll"]
