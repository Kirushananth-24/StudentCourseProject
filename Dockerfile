# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy csproj and restore dependencies
COPY *.sln .
COPY StudentCourseAPI/*.csproj ./StudentCourseAPI/
RUN dotnet restore

# Copy the rest of the code
COPY StudentCourseAPI/. ./StudentCourseAPI/
WORKDIR /app/StudentCourseAPI

# Build and publish the app
RUN dotnet publish -c Release -o out

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/StudentCourseAPI/out ./

# Expose the port
EXPOSE 80

# Set the entrypoint
ENTRYPOINT ["dotnet", "StudentCourseAPI.dll"]
