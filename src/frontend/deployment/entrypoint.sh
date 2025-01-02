#!/bin/bash

getent group developers 

export PNPM_HOME="/pnpm"
export PATH="$PNPM_HOME:$PATH"

corepack enable

cd ./src/frontend || exit

ls /opt/bitnami/node/bin
echo '----'
ls /opt/bitnami/node
echo '----'
ls /opt/bitnami
echo '----'
ls /opt/bitnami/lib
echo '----'

pnpm --version

pnpm run start

cd ./../.. || exit