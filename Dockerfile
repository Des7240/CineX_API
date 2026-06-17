# Stage 1: Base image dùng để chạy ứng dụng (chỉ chứa Runtime)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
# Port mặc định của ASP.NET Core 8/9 là 8080
EXPOSE 8080 
EXPOSE 8081

# Stage 2: Build image dùng để biên dịch source code (chứa SDK)
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy file .csproj và restore các packages
COPY ["CineX_API.csproj", "./"]
RUN dotnet restore "./CineX_API.csproj"

# Copy toàn bộ mã nguồn và tiến hành build
COPY . .
WORKDIR "/src/."
RUN dotnet build "CineX_API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Stage 3: Publish ứng dụng
FROM build AS publish
RUN dotnet publish "CineX_API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Stage 4: Tạo image cuối cùng
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Thiết lập môi trường Production
ENV ASPNETCORE_ENVIRONMENT=Production

# Khởi chạy ứng dụng
ENTRYPOINT ["dotnet", "CineX_API.dll"]
