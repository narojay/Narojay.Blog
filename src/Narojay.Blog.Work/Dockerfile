FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Narojay.Blog.Work/Narojay.Blog.Work.csproj", "Narojay.Blog.Work/"]
COPY ["Narojay.Blog.Infrastruct/Narojay.Blog.Infrastruct.csproj", "Narojay.Blog.Infrastruct/"]
COPY ["Narojay.Blog.Domain/Narojay.Blog.Domain.csproj", "Narojay.Blog.Domain/"]
COPY ["EventBus/EventBusRabbitMQ/EventBusRabbitMQ/EventBusRabbitMQ.csproj", "EventBusRabbitMQ/"]
COPY ["EventBus/EventBus/EventBus.csproj", "EventBus/"]
COPY ["Narojay.Blog.Application/Narojay.Blog.Application.csproj", "Narojay.Blog.Application/"]
RUN dotnet restore "Narojay.Blog.Work/Narojay.Blog.Work.csproj"
COPY . .
WORKDIR "/src/Narojay.Blog.Work"
RUN dotnet build "Narojay.Blog.Work.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Narojay.Blog.Work.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Narojay.Blog.Work.dll"]
