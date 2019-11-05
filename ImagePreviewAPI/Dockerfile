FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
RUN apt-get update;apt-get install libfontconfig1 -y

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ImagePreviewAPI/ImagePreviewAPI.csproj ImagePreviewAPI/
RUN dotnet restore "ImagePreviewAPI/ImagePreviewAPI.csproj"
COPY . .
WORKDIR "/src/ImagePreviewAPI"
RUN dotnet build "ImagePreviewAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ImagePreviewAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ImagePreviewAPI.dll"]
