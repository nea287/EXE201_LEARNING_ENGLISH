#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["EXE201_LEARNING_ENGLISH_API/EXE201_LEARNING_ENGLISH_API.csproj", "EXE201_LEARNING_ENGLISH_API/"]
COPY ["EXE201_LEARNING_ENGLISH_BusinessLayer/EXE201_LEARNING_ENGLISH_BusinessLayer.csproj", "EXE201_LEARNING_ENGLISH_BusinessLayer/"]
COPY ["EXE201_LEARNING_ENGLISH_Repository/EXE201_LEARNING_ENGLISH_Repository.csproj", "EXE201_LEARNING_ENGLISH_Repository/"]
COPY ["EXE201_LEARNING_ENGLISH_DAO/EXE201_LEARNING_ENGLISH_DAO.csproj", "EXE201_LEARNING_ENGLISH_DAO/"]
COPY ["EXE201_LEARNING_ENGLISH_DataLayer/EXE201_LEARNING_ENGLISH_DataLayer.csproj", "EXE201_LEARNING_ENGLISH_DataLayer/"]
RUN dotnet restore "EXE201_LEARNING_ENGLISH_API/EXE201_LEARNING_ENGLISH_API.csproj"
COPY . .
WORKDIR "/src/EXE201_LEARNING_ENGLISH_API"
RUN dotnet build "EXE201_LEARNING_ENGLISH_API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EXE201_LEARNING_ENGLISH_API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EXE201_LEARNING_ENGLISH_API.dll"]