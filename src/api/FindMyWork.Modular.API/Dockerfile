FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY ["api/FindMyWork.Modular.API/FindMyWork.Modular.API.csproj", "api/FindMyWork.Modular.API/"]

COPY ["modules/jobs/FIndMyWork.Modules.Jobs.Api/FIndMyWork.Modules.Jobs.Api.csproj", "modules/jobs/FindMyWork.Modules.Jobs.Api/"]
COPY ["modules/jobs/FindMyWork.Modules.Jobs.Core/FindMyWork.Modules.Jobs.Core.csproj", "modules/jobs/FindMyWork.Modules.Jobs.Core/"]

COPY ["shared/FindMyWork.Shared.Application/FindMyWork.Shared.Application.csproj", "shared/FindMyWork.Shared.Application/"]
COPY ["shared/FindMyWork.Shared.Infrastructure/FindMyWork.Shared.Infrastructure.csproj", "shared/FindMyWork.Shared.Infrastructure/"]

RUN dotnet restore "api/FindMyWork.Modular.API/FindMyWork.Modular.API.csproj"

COPY . .
WORKDIR "/src/api/FindMyWork.Modular.API"
RUN dotnet build "FindMyWork.Modular.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FindMyWork.Modular.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FindMyWork.Modular.API.dll"]
