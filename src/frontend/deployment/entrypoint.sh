#!/bin/bash

getent group developers 

export PNPM_HOME="/pnpm"
export PATH="$PNPM_HOME:$PATH"

cd ./src/frontend || exit

ls ../lib/node_modules

corepack enable

ls ../lib/node_modules/corepack/

pnpm --version

pnpm run start

cd ./../.. || exit