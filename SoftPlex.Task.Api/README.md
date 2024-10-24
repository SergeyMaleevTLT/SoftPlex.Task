# Запуск приложения

1. Приложение содержит в себе файлы .sql для инициализации базы данных.

Для инициализации БД небходимо перейди в корневой каталог сборки и выполнить одну из команд:

MSSQL выполнить команду
```bash 
sqlcmd -S сервер -U пользователь -P пароль -i ./CLI.Migrations/SqlScripts/DatabaseMsSql.sql
```

PSql выполнить команду
```bash 
psql -U пользователь -P пароль -f ./CLI.Migrations/SqlScripts/DatabasePgs.sql
```
При возникновении ошибок скопируйте sql скрипт и выполните в среде администрирования и разработки необходимой базы данных

2. В файле appsettings.json указать строку подключения к БД и провайдер

Доступные варианты Provider БД:
 - PostgreSqlEfDbProvider (работа c PosgreSql)
 - MSSqlEfDbProvider (работа c Microsoft SQL Server)
```bash 
"ConnectionStrings": {
    "Data": "",
    "Provider": ""
  }
```



