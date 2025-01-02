#!/bin/bash

export GITHUB_CLIENT_ID=$1
export NODE_DEBUG=net

corepack enable

cd ./src/frontend || exit

pnpm install --prod --frozen-lockfile
pnpm install --frozen-lockfile

pnpm run build

cd ./../.. || exit