################
## Build App ###
################
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY . .
RUN dotnet --version
RUN dotnet restore "Kirschhock.HTIYC/Kirschhock.HTIYC.csproj"
RUN dotnet build "Kirschhock.HTIYC/Kirschhock.HTIYC.csproj" -c Release
RUN dotnet publish "Kirschhock.HTIYC/Kirschhock.HTIYC.csproj" -c Release -o /app/publish

#############
## Host App ##
##############
FROM mcr.microsoft.com/dotnet/aspnet:5.0 
WORKDIR /app
EXPOSE 80
ENV ASPNETCORE_URLS http://+:80
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Kirschhock.HTIYC.dll"]
