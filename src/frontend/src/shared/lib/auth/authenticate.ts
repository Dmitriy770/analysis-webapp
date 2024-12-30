'use server'

import { cache } from 'react'
import { authApi, type User } from '~/shared/lib/api'

export const authenticate = cache(async (): Promise<User | null> => {
  return await authApi.getUser()
})
