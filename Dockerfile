FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["MyProject.csproj", "."]
RUN dotnet restore "MyProject.csproj"
COPY . .
RUN dotnet publish "MyProject.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
EXPOSE 80
ENV ASPNETCORE_URLS=http://+:80
COPY --from=build /app/publish .
COPY ./wwwroot/media .app/wwwroot/media

ENTRYPOINT ["dotnet", "MyProject.dll"]
