import axios from 'axios'
import { type ZodSchema, z } from 'zod'
import type { User } from './types'
import { sessionIdRequestInterceptor, updateSessionCookie } from '../axios'

const client = axios.create({
  baseURL: 'http://api.analysis.devsquare.ru',
  validateStatus: () => true,
})

client.interceptors.request.use(sessionIdRequestInterceptor)

const userSchema: ZodSchema<User> = z.object({
  nickname: z.string().min(1),
  avatarUrl: z.string().url().optional(),
})

export const postLogin = async (githubCode: string): Promise<User | null> => {
  const response = await client.post('/user/login', { token: githubCode })

  if (response.status !== 200) {
    return null
  }

  // NOTE: next умеет обновлять cookie только в action или route.ts,
  // а по-хорошему надо уметь на любом запросе.
  updateSessionCookie(response)

  const parsedUser = userSchema.safeParse(response.data)
  if (!parsedUser.success) {
    return null
  }

  return parsedUser.data
}

export const postLogout = async () => {
  await client.post('/user/logout')
}

export const getUser = async (): Promise<User | null> => {
  const response = await client.get('/user')

  if (response.status !== 200) {
    return null
  }

  const parsedUser = userSchema.safeParse(response.data)

  return parsedUser.success ? parsedUser.data : null
}
