import { cookies } from 'next/headers'
import type { AxiosRequestInterceptor, AxiosResponseInterceptor } from './types'

const SESSION_ID_COOKIE = 'SessionId'

export const sessionIdRequestInterceptor: AxiosRequestInterceptor = async request => {
  const nextCookies = await cookies()

  const sessionIdCookie = nextCookies.get(SESSION_ID_COOKIE)

  if (sessionIdCookie) {
    request.headers.Cookie = `${SESSION_ID_COOKIE}=${sessionIdCookie.value};`
  }

  return request
}

export const updateSessionCookie: AxiosResponseInterceptor = async response => {
  const sessionId = (response.headers['set-cookie'] ?? [])
    .flatMap(cookie => cookie.split(';').map(attr => attr.split('=', 2) as [string, string]))
    .find(attr => attr[0] === SESSION_ID_COOKIE)?.[1]

  if (sessionId) {
    const nextCookies = await cookies()

    nextCookies.set(SESSION_ID_COOKIE, sessionId, { path: '/', httpOnly: true })
  }

  return response
}
