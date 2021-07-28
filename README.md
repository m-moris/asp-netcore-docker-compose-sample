# ASP.NET の docker compose サンプル

## Dockerfileなど

以下は、Visual Studio 2019による自動生成ファイル。追加メニューより追加できる。

* Dockerfile
* docker-compose*.yaml

## 構成

**FrontEnd**

以下をAPI提供。`hello2` は、バックエンドのAPIを呼び出す。

* hello
* hello2

**BackEnd**

以下のAPIを提供、よくある WeathrForecast のAPI短縮版

* w

## 動作手順

**実行**

```
docker-compose build
docker-compose up
```

**停止**

```
docker-compose down
```

**確認**

```
docker-compose ps
```

**curl**

`-k` オプションで証明書を無視する

```
curl https://localhost:8888/hello -k
curl https://localhost:8888/hello2 -k
```

## Azure Web Appsへのデプロイ

Docker hub にログインしつつ、タグ付けし、プッシュする。

```
docker login
docker tag frontend morishima/frontend:latest
docker tag backend  morishima/backend:latest
docker-compose -f azure-compose.yml push
```
初期設定とリソースグループの作成

```
rg=webapps-test
az configure --defaults location=japaneast
az configure --defaults group=$rg
az group create -n $rg
```

App Service Plan の作成

```
plan=containerplan
az appservice plan create -n $plan --sku S1 --is-linux
```

App Service の作成とデプロイ。既存のリソースがある場合は、設定を上書きしてくれる。

```
az webapp create --plan $plan --name mycontainer20210808 \
                 --multicontainer-config-type compose \
                 --multicontainer-config-file azure-compose.yml
```

APIを叩く。

```
curl http://mycontainer20210808.azurewebsites.net/hello
curl http://mycontainer20210808.azurewebsites.net/hello2
```

### 注意

* override ファイルのような複数ファイル指定はできない（VSで複数生成するのに）
* 現在、Azure App Service の Docker Compose には 4,000 文字の制限がある

### HTTPS

App Serviceは、App Service フロントエンドで SSLの通信を終了するので、コンテナ側は80のみで問題ないというか（コンテナ側で443を実装してはいけないし、80/8080以外は無視されるようだ）

したがって、80だけ設定しておけばよく、https で接続しても80 にポートフォワードされる。

## 参考

[Docker Compose を使用して複数のコンテナーを使用する - Visual Studio (Windows) | Microsoft Docs](https://docs.microsoft.com/ja-jp/visualstudio/containers/tutorial-multicontainer?view=vs-2019)

[カスタム コンテナーを構成する - Azure App Service | Microsoft Docs](https://docs.microsoft.com/ja-jp/azure/app-service/configure-custom-container?pivots=container-linux#detect-https-session)