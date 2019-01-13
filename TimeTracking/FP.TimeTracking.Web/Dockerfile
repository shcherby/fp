FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["FP.TimeTracking.Web/FP.TimeTracking.Web.fsproj", "FP.TimeTracking.Web/"]
RUN dotnet restore "FP.TimeTracking.Web/FP.TimeTracking.Web.fsproj"
COPY . .
WORKDIR "/src/FP.TimeTracking.Web"
RUN dotnet build "FP.TimeTracking.Web.fsproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "FP.TimeTracking.Web.fsproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "FP.TimeTracking.Web.dll"]
