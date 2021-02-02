@echo off

docker stop mebrak > NUL 2> NUL
docker rm mebrak > NUL 2> NUL

cd ..

copy docker\mebrak.dockerfile mebrak.dockerfile > NUL
docker build . -f mebrak.dockerfile -t darakeon/mebrak
del mebrak.dockerfile > NUL

cd docker

docker compose up --detach --project-name mebrak

REM docker push darakeon/mebrak
