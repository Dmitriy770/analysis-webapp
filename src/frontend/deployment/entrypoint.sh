#!/bin/bash

export PNPM_HOME="/pnpm"
export PATH="$PNPM_HOME:$PATH"

cd ./src/frontend || exit

corepack enable

pnpm run start

cd ./../.. || exit