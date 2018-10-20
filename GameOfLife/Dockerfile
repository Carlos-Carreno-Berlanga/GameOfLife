FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 51536
EXPOSE 44374

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
RUN curl -sL https://deb.nodesource.com/setup_10.x |  bash -
RUN apt-get install -y nodejs
COPY ["GameOfLife/GameOfLife.csproj", "GameOfLife/"]
RUN dotnet restore "GameOfLife/GameOfLife.csproj"
COPY . .
WORKDIR "/src/GameOfLife"
RUN dotnet build "GameOfLife.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "GameOfLife.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
CMD ASPNETCORE_URLS=http://*:$PORT dotnet GameOfLife.dll