#!/bin/bash

getent group developers 

export PNPM_HOME="/pnpm"
export PATH="$PNPM_HOME:$PATH"

cd ./src/frontend || exit

corepack enable

ls /opt/bitnami/node/bin/pnpm
echo '----'
ls /opt/bitnami/node
echo '----'
ls /opt/bitnami
echo '----'

pnpm --version

pnpm run start

cd ./../.. || exit