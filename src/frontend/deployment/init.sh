#!/bin/bash

export GITHUB_CLIENT_ID=$1
echo 'client id '
echo $0
echo $1
echo $2
echo $GITHUB_CLIENT_ID

corepack enable

cd ./src/frontend || exit

pnpm install --prod --frozen-lockfile
pnpm install --frozen-lockfile

pnpm run build

cd ./../.. || exit