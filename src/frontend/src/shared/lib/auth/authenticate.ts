import { cookies } from 'next/headers'
import { cache } from 'react'
import type { User } from '~/shared/lib/api'

export const authenticate = cache(async (): Promise<User | null> => {
  await cookies()

  // TODO: check the session id cookie

  return null
})
