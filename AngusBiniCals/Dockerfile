#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 9000
ENV ASPNETCORE_URLS=http://*:9000

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AngusBiniCals/AngusBiniCals.csproj", "AngusBiniCals/"]
COPY ["GovServiceUtilities/GovServiceUtilities.csproj", "GovServiceUtilities/"]
RUN dotnet restore "AngusBiniCals/AngusBiniCals.csproj"
COPY . .
WORKDIR "/src/AngusBiniCals"
RUN dotnet build "AngusBiniCals.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AngusBiniCals.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AngusBiniCals.dll"]