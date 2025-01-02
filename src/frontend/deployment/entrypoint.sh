#!/bin/bash

export PNPM_HOME="$HOME/bin/pnpm"
export PATH="$PNPM_HOME:$PATH"

cd ./src/frontend || exit

corepack enable --install-directory ~/bin

pnpm --version

pnpm run start

cd ./../.. || exit