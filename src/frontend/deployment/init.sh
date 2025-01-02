#!/bin/bash

export PNPM_HOME="/pnpm"
export PATH="$PNPM_HOME:$PATH"

corepack enable

cd ./src/frontend || exit

pnpm install --prod --frozen-lockfile
pnpm install --frozen-lockfile

pnpm run build

cd ./../.. || exit