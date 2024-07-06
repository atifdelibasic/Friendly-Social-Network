#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Friendly/Friendly.WebAPI.csproj", "Friendly.WebAPI/"]
COPY ["Friendly.Service/Friendly.Service.csproj", "Friendly.Service/"]
COPY ["Friendly.Model/Friendly.Model.csproj", "Friendly.Model/"]
COPY ["Friendly.Database/Friendly.Database.csproj", "Friendly.Database/"]
RUN dotnet restore "Friendly.WebAPI/Friendly.WebAPI.csproj"
COPY . .
WORKDIR "/src/Friendly"
RUN dotnet build "Friendly.WebAPI.csproj" -c Release -o /app
EXPOSE 7169

FROM build AS publish
RUN dotnet publish "Friendly.WebAPI.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Friendly.WebAPI.dll"]