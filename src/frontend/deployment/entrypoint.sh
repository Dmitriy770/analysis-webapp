#!/bin/bash

export PNPM_HOME="/pnpm"
export PATH="$PNPM_HOME:$PATH"

cd ./src/frontend || exit

ls ../lib/node_modules/corepack/

corepack enable

pnpm --version

pnpm run start

cd ./../.. || exit