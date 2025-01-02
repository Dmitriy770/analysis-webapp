#!/bin/bash

getent group developers 

export PNPM_HOME="/pnpm"
export PATH="$PNPM_HOME:$PATH"

npm install -g pnpm

corepack enable

cd ./src/frontend || exit

ls /opt/bitnami/node/bin/pnpm
echo '----'
ls /opt/bitnami/node/bin/lib/node_modules/corepack/dist
echo '----'
ls /opt/bitnami/node/bin/lib/node_modules/corepack
echo '----'
ls /opt/bitnami/node/bin/lib/node_modules
echo '----'
ls /opt/bitnami/node/bin/lib
echo '----'
ls /opt/bitnami/node/bin
echo '----'
ls /opt/bitnami/node/bin/node
echo '----'
ls /opt/bitnami/node
echo '----'


pnpm --version

pnpm run start

cd ./../.. || exit