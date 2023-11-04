 FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["MyProject.csproj", "./"]
RUN dotnet restore "MyProject.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet publish "MyProject.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
EXPOSE 80
ENV ASPNETCORE_URLS=http://+:80
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "MyProject.dll"]
