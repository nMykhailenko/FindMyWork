FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/web/FindMyWork.Modular.Identity.Web/FindMyWork.Modular.Identity.Web.csproj", "FindMyWork.Modular.Identity.Web/"]
RUN dotnet restore "src/web/FindMyWork.Modular.Identity.Web/FindMyWork.Modular.Identity.Web.csproj"
COPY . .
WORKDIR "/src/FindMyWork.Modular.Identity.Web"
RUN dotnet build "FindMyWork.Modular.Identity.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FindMyWork.Modular.Identity.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FindMyWork.Modular.Identity.Web.dll"]
