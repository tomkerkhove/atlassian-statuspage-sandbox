#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["TomKerkhove.Sandbox.MetricGenerator/TomKerkhove.Sandbox.MetricGenerator.csproj", "TomKerkhove.Sandbox.MetricGenerator/"]
RUN dotnet restore "TomKerkhove.Sandbox.MetricGenerator/TomKerkhove.Sandbox.MetricGenerator.csproj"
COPY . .
WORKDIR "/src/TomKerkhove.Sandbox.MetricGenerator"
RUN dotnet build "TomKerkhove.Sandbox.MetricGenerator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TomKerkhove.Sandbox.MetricGenerator.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TomKerkhove.Sandbox.MetricGenerator.dll"]