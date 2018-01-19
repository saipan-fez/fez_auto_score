# DB更新手順

DBのテーブル構成に変更があった場合には、下記を実施してください。

1. (初回のみ)パッケージマネージャから下記のコマンドを実行  
`PM> Enable-Migrations`
2. FodyWeavers.xml内のコメントアウトを外す
3. パッケージマネージャから下記のコマンドを実行  
`PM> Add-Migration 変更内容`  
`PM> Update-Database`
5. FodyWeavers.xml内のコメントアウトを戻す