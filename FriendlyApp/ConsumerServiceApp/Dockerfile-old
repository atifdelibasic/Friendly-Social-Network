# Use the .NET SDK image for building the application
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Set the working directory inside the container
WORKDIR /app

# Restore dependencies by copying and restoring the project file
COPY ./ConsumerServiceApp/*.csproj ./
RUN dotnet restore "./ConsumerServiceApp.csproj"

# Build the application
COPY ./ConsumerServiceApp/. ./
RUN dotnet publish -c Release -o out

# Use the .NET runtime image for running the application
FROM mcr.microsoft.com/dotnet/runtime:6.0 AS runtime

# Set the working directory inside the container
WORKDIR /app

# Copy the published application from the build stage
COPY --from=build /app/out ./

# Specify the command to run the application
CMD ["dotnet", "ConsumerServiceApp.dll"]
