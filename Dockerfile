FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY ./output .
ENV ASPNETCORE_ENVIRONMENT=Development
ENV DOTNET_URLS=http://+:5213
ENTRYPOINT ["dotnet", "LibraryLoans.Api.dll"]
