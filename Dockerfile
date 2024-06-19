FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj ./

RUN dotnet restore

#Copy the rest of application to container
COPY . ./

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS runtime

WORKDIR /app

COPY --from=build /app/out ./

ENTRYPOINT [ "dotnet","test.dll" ]