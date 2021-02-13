@echo off

docker context use default

docker stop mebrak > NUL 2> NUL
docker rm mebrak > NUL 2> NUL

cd ..

copy mebrak\mebrak.dockerfile mebrak.dockerfile > NUL
docker build . -f mebrak.dockerfile -t darakeon/mebrak
del mebrak.dockerfile > NUL

cd mebrak

docker compose up --project-name mebrak --detach
