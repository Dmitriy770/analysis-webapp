import { RedirectType, redirect } from 'next/navigation'
import type { NextRequest } from 'next/server'
import { authApi } from '~/shared/lib/api'
import { env } from '~/shared/lib/env'

const GITHUB_OAUTH_URL = `https://github.com/login/oauth/authorize?scope=user&client_id=${env.GITHUB_CLIENT_ID}`

export const GET = async (request: NextRequest) => {
  const {
    nextUrl: { searchParams },
  } = request

  const githubCode = searchParams.get('code')

  if (!githubCode) {
    redirect(GITHUB_OAUTH_URL, RedirectType.replace)
  }

  const user = await authApi.postLogin(githubCode)

  redirect(user ? '/dashboard' : '/', RedirectType.replace)
}
