import { z } from 'zod'

const envSchema = z.object({
  GITHUB_CLIENT_ID: z.string(),
})

export const env = envSchema.parse(process.env)
