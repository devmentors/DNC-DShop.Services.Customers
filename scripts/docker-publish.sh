#!/bin/bash
DOCKER_ENV=''
DOCKER_TAG=''

case "$TRAVIS_BRANCH" in
  "master")
    DOCKER_ENV=production
    DOCKER_TAG=latest
    ;;
  "develop")
    DOCKER_ENV=development
    DOCKER_TAG=dev
    ;;    
esac

docker login -u $DOCKER_USERNAME -p $DOCKER_PASSWORD
docker build -f ./src/DShop.Services.Customers/Dockerfile.$DOCKER_ENV -t dshop.services.customers:$DOCKER_TAG ./src/DShop.Services.Customers
docker tag dshop.services.customers:$DOCKER_TAG $DOCKER_USERNAME/dshop.services.customers:$DOCKER_TAG
docker push $DOCKER_USERNAME/dshop.services.customers:$DOCKER_TAG