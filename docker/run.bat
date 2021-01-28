@echo off

cd ..

copy docker\mebrak.dockerfile mebrak.dockerfile > NUL
docker build . -f mebrak.dockerfile -t darakeon/mebrak
del mebrak.dockerfile > NUL

if %errorlevel% neq 0 exit /b %errorlevel%

copy docker\nginx.dockerfile nginx.dockerfile > NUL
docker build . -f nginx.dockerfile -t darakeon/nginx
del nginx.dockerfile > NUL

if %errorlevel% neq 0 exit /b %errorlevel%

cd docker

docker compose down
docker compose up --detach --project-name mebrak

REM docker push darakeon/nginx
REM docker push darakeon/mebrak
