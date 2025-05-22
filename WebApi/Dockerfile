FROM mcr.microsoft.com/dotnet/sdk:8.0 AS dotnet-build

WORKDIR /build

COPY . .

RUN ["dotnet", "restore"]
RUN ["dotnet", "publish", "-c", "Release", "-o", "out"]


FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS dotnet-runtime

WORKDIR /run

COPY --from=dotnet-build /build/out .

ENV ASPNETCORE_URLS=http://+:5001

EXPOSE 5001

ENTRYPOINT [ "dotnet", "WebApi.dll" ]

LABEL description="Dotnet WebApi"
LABEL author="Lorin Steiner"
