#!/bin/bash

# 创建 .env 文件
touch .env

# 询问并读取数据库密码
echo "Database Password："
read -s DB_PASSWORD

# 询问并读取证书密码
echo "certificate Password："
read -s CERT_PASSWORD

# 询问并读取 Seq 管理员密码，然后使用 Docker 命令生成其哈希值
echo "Seq Password："
read -s SEQ_ADMIN_INPUT
SEQ_ADMIN_PASSWORD=$(echo "$SEQ_ADMIN_INPUT" | docker run --rm -i datalust/seq config hash)

# 将环境变量写入 .env 文件
echo "DB_PASSWORD=$DB_PASSWORD" > .env
echo "CERT_PASSWORD=$CERT_PASSWORD" >> .env
echo "SEQ_ADMIN_PASSWORD=$SEQ_ADMIN_PASSWORD" >> .env

# 输出结果以验证
echo "Created .env"
#cat .env
