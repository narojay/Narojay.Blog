FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
ENV ASPNETCORE_URLS=http://*:5000
ENV ASPNETCORE_ENVIRONMENT=Development

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Narojay.Blog/Narojay.Blog.csproj", "Narojay.Blog/"]
COPY ["EventBus/EventBusRabbitMQ/EventBusRabbitMQ/EventBusRabbitMQ.csproj", "EventBusRabbitMQ/"]
COPY ["EventBus/EventBus/EventBus.csproj", "EventBus/"]
COPY ["Narojay.Blog.Application/Narojay.Blog.Application.csproj", "Narojay.Blog.Application/"]
COPY ["Narojay.Blog.Infrastruct/Narojay.Blog.Infrastruct.csproj", "Narojay.Blog.Infrastruct/"]
COPY ["Narojay.Blog.Domain/Narojay.Blog.Domain.csproj", "Narojay.Blog.Domain/"]
RUN dotnet restore "Narojay.Blog/Narojay.Blog.csproj"
COPY . .
WORKDIR "/src/Narojay.Blog"
RUN dotnet build "Narojay.Blog.csproj"  -o /app/build

FROM build AS publish
RUN dotnet publish "Narojay.Blog.csproj"  -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Narojay.Blog.dll"]
