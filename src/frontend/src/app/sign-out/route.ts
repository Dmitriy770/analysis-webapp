import { cookies } from 'next/headers'
import { redirect, RedirectType } from 'next/navigation'
import { authApi } from '~/shared/lib/api'

export const GET = async () => {
  await authApi.postLogout()

  const nextCookies = await cookies()

  nextCookies.delete('SessionId')

  redirect('/', RedirectType.replace)
}
