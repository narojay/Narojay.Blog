FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /narojayblog

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Narojay.Blog/Narojay.Blog.csproj", "Narojay.Blog/"]
COPY ["src/EventBus/EventBusRabbitMQ/EventBusRabbitMQ/EventBusRabbitMQ.csproj", "EventBusRabbitMQ/"]
COPY ["src/EventBus/EventBus/EventBus.csproj", "EventBus/"]
COPY ["src/Narojay.Blog.Application/Narojay.Blog.Application.csproj", "Narojay.Blog.Application/"]
COPY ["src/Narojay.Blog.Infrastruct/Narojay.Blog.Infrastruct.csproj", "Narojay.Blog.Infrastruct/"]
COPY ["src/Narojay.Blog.Domain/Narojay.Blog.Domain.csproj", "Narojay.Blog.Domain/"]
RUN dotnet restore "Narojay.Blog/Narojay.Blog.csproj"
COPY src/. .
RUN dotnet build "Narojay.Blog/Narojay.Blog.csproj" -c Release -o /narojayblog/build

FROM build AS publish
RUN dotnet publish "Narojay.Blog/Narojay.Blog.csproj" -c Release -o /narojayblog/publish

FROM base AS final
WORKDIR /narojayblog
COPY --from=publish /narojayblog/publish .
ENTRYPOINT ["dotnet", "Narojay.Blog.dll"]
