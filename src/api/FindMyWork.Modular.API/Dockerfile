FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/FindMyWork.Modular.API/FindMyWork.Modular.API.csproj", "FindMyWork.Modular.API/"]
RUN dotnet restore "src/FindMyWork.Modular.API/FindMyWork.Modular.API.csproj"
COPY . .
WORKDIR "/src/FindMyWork.Modular.API"
RUN dotnet build "FindMyWork.Modular.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FindMyWork.Modular.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FindMyWork.Modular.API.dll"]
