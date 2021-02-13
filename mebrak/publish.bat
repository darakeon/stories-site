@echo off

docker context use default
docker push darakeon/mebrak

docker context use AWS
docker compose up --project-name mebrak

docker context use default

