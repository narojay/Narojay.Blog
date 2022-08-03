FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /narojaygateway

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Narojay.Blog.Gateways/Narojay.Blog.Gateways.csproj", "Narojay.Blog.Gateways/"]
RUN dotnet restore "Narojay.Blog.Gateways/Narojay.Blog.Gateways.csproj"
COPY . .
WORKDIR "/src/Narojay.Blog.Gateways"
RUN dotnet build "Narojay.Blog.Gateways.csproj" -c Release -o /narojaygateway/build

FROM build AS publish
RUN dotnet publish "Narojay.Blog.Gateways.csproj" -c Release -o /narojaygateway/publish

FROM base AS final
WORKDIR /narojaygateway
COPY --from=publish /narojaygateway/publish .
ENTRYPOINT ["dotnet", "Narojay.Blog.Gateways.dll"]
