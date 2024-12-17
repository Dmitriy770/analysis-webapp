import axios from 'axios'
import type { User } from './types'
import { z } from 'zod'

const client = axios.create({
  baseURL: 'http://api.analysis.devsquare.ru/api',
})

const loginResponseSchema = z.object({
  nickname: z.string().min(1),
  avatarUrl: z.string().url().optional(),
})

export const postLogin = async (githubCode: string): Promise<User | null> => {
  const response = await client.post('/user/login', { token: githubCode })

  if (response.status !== 200) {
    return null
  }

  const parsedUser = loginResponseSchema.safeParse(response.data)
  if (!parsedUser.success) {
    return null
  }

  return parsedUser.data
}
