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

**curl***

`-k` オプションで証明書を無視する

```
curl https://localhost:8888/hello -k
curl https://localhost:8888/hello2 -k
```

## 参考

[Docker Compose を使用して複数のコンテナーを使用する - Visual Studio (Windows) | Microsoft Docs](https://docs.microsoft.com/ja-jp/visualstudio/containers/tutorial-multicontainer?view=vs-2019)
