FROM node:18-alpine AS client-build
WORKDIR /client-app

# 拷贝前端应用的 package.json 和 package-lock.json（如果存在）
COPY ClientApp/package*.json ./

# 安装依赖项
RUN npm install --legacy-peer-deps

# 拷贝前端应用的源代码
COPY ClientApp/ ./

# 构建前端应用
RUN npm run build

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/WebAPI/WebAPI.csproj", "src/WebAPI/"]
COPY ["src/Application/Application.csproj", "src/Application/"]
COPY ["src/Domain/Domain.csproj", "src/Domain/"]
COPY ["src/Infrastructure/Infrastructure.csproj", "src/Infrastructure/"]
COPY ["ClientApp","/"]

RUN dotnet restore "src/WebAPI/WebAPI.csproj"
COPY . .
WORKDIR "/src/src/WebAPI"
RUN dotnet build "WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=client-build /client-app/dist /app/wwwroot
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebAPI.dll"]

# 选择一个包含 Node.js 的基础镜像
# 第一阶段：使用 Node.js 镜像构建前端应用
#FROM node:18-alpine AS client-build
#WORKDIR /client-app
#
## 拷贝前端应用的 package.json 和 package-lock.json（如果存在）
#COPY ClientApp/package*.json ./
#
## 安装依赖项
#RUN npm install --legacy-peer-deps
#
## 拷贝前端应用的源代码
#COPY ClientApp/ ./
#
## 构建前端应用
#RUN npm run build
#
## 第二阶段：使用 .NET SDK 镜像构建后端应用
#FROM mcr.microsoft.com/dotnet/sdk:6.0 AS server-build
#WORKDIR /src
#
## 复制 csproj 文件并还原 .NET 依赖项
#COPY ["src/WebAPI/WebAPI.csproj", "WebAPI/"]
#COPY ["src/Application/Application.csproj", "Application/"]
#COPY ["src/Domain/Domain.csproj", "Domain/"]
#COPY ["src/Infrastructure/Infrastructure.csproj", "Infrastructure/"]
#RUN dotnet restore "WebAPI/WebAPI.csproj"
#
## 复制后端应用的源代码
#COPY . .
#WORKDIR /src/WebAPI
#
## 构建后端应用
#RUN dotnet publish "WebAPI.csproj" -c Release -o /app/build /p:UseAppHost=false
#
## 第三阶段：设置运行时环境
#FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
#WORKDIR /app
#EXPOSE 80
#EXPOSE 443
#EXPOSE 8080
#
## 从 server-build 阶段拷贝构建输出到最终镜像
#COPY --from=server-build /app/build .
#
## 从 client-build 阶段拷贝构建的前端应用到最终镜像的 wwwroot
#COPY --from=client-build /client-app/dist ./wwwroot
#
## 环境变量可在运行容器时设置，或在此处预先定义
## ENV ASPNETCORE_URLS=http://*:8080
## ENV VITE_API_BASE_URL=your-api-base-url
#
#ENTRYPOINT ["dotnet", "WebAPI.dll"]
