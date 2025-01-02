#!/bin/bash

export PNPM_HOME="/pnpm"
export PATH="$PNPM_HOME:$PATH"

cd ./src/frontend || exit

corepack enable

pnpm install --prod --frozen-lockfile
pnpm install --frozen-lockfile

cd ./../.. || exit