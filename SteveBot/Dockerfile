FROM microsoft/dotnet-framework:4.7.2-sdk-windowsservercore-1803 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln .
COPY <path-to>/*.csproj ./<path-to>/
COPY <path-to>/*.config ./<path-to>/
RUN nuget restore

# copy everything else and build app
COPY <path-to>/. ./<path-to>/
WORKDIR /app/<path-to>
RUN msbuild /p:Configuration=Release


FROM microsoft/aspnet:4.7.2-windowsservercore-1803 AS runtime
WORKDIR /inetpub/wwwroot
COPY --from=build /app/<path-to>/. ./