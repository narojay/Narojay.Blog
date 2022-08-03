#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Narojay.Blog.Gateways/Narojay.Blog.Gateways.csproj", "src/Narojay.Blog.Gateways/"]
RUN dotnet restore "src/Narojay.Blog.Gateways/Narojay.Blog.Gateways.csproj" -r linux-x64
COPY . .
WORKDIR "/src/src/Narojay.Blog.Gateways"
RUN dotnet build "Narojay.Blog.Gateways.csproj" -c Release -o /app/build -r linux-x64 --no-restore

FROM build AS publish
RUN dotnet publish "Narojay.Blog.Gateways.csproj" -c Release -o /app/publish -r linux-x64 --no-restore

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Narojay.Blog.Gateways.dll"]