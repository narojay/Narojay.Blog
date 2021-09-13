FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
COPY . .
ENTRYPOINT ["dotnet", "Narojay.Blog.dll"]
