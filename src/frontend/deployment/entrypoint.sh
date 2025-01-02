#!/bin/bash

export NODE_DEBUG=net

corepack enable

cd ./src/frontend || exit

pnpm run start