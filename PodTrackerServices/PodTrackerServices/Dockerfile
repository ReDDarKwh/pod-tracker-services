FROM microsoft/dotnet:2.1-sdk-stretch-arm32v7 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY PodTrackerServices/*.csproj ./PodTrackerServices/
COPY utils/*.csproj ./utils/
WORKDIR /app/PodTrackerServices
RUN dotnet restore

# copy and build app and libraries
WORKDIR /app/
COPY PodTrackerServices/. ./PodTrackerServices/
COPY utils/. ./utils/
WORKDIR /app/PodTrackerServices
# add IL Linker package
RUN dotnet add package ILLink.Tasks -v 0.1.5-preview-1841731 -s https://dotnet.myget.org/F/dotnet-core/api/v3/index.json
RUN dotnet publish -c Release -r linux-arm -o out /p:ShowLinkerSizeComparison=true


# test application -- see: dotnet-docker-unit-testing.md
FROM build AS testrunner
WORKDIR /app/tests
COPY tests/. .
ENTRYPOINT ["dotnet", "test", "--logger:trx"]


FROM microsoft/dotnet:2.1-runtime-deps-stretch-slim-arm32v7 AS runtime
WORKDIR /app
COPY --from=build /app/PodTrackerServices/out ./
ENTRYPOINT ["./PodTrackerServices"]
