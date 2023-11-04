FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["MyProject.csproj", "./"]
RUN dotnet restore "MyProject.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet publish "MyProject.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app/publish .
COPY ./wwwroot/media ./wwwroot/media

ENTRYPOINT ["dotnet", "MyProject.dll"]
