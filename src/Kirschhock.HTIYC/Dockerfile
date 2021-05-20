#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["howtoimpressyourcolleagues/Kirschhock.HTIYC/Kirschhock.HTIYC.csproj", "howtoimpressyourcolleagues/Kirschhock.HTIYC/"]
RUN dotnet restore "howtoimpressyourcolleagues/Kirschhock.HTIYC/Kirschhock.HTIYC.csproj"
COPY . .
WORKDIR "/src/howtoimpressyourcolleagues/Kirschhock.HTIYC"
RUN dotnet build "Kirschhock.HTIYC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Kirschhock.HTIYC.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Kirschhock.HTIYC.dll"]