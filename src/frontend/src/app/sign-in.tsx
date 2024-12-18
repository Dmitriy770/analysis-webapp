'use server'

import type { FC } from 'react'
import { buttonVariants, type ButtonProps } from '~/shared/components/button'
import { DashboardIcon, SignInIcon } from '~/shared/icons'
import { authenticate } from '~/shared/lib/auth/authenticate'
import { cn } from '~/shared/lib/classnames'

type Props = ButtonProps

export const SignIn: FC<Props> = async ({ className, ...props }) => {
  const user = await authenticate()

  return (
    <a
      href={user ? '/dashboard' : '/api/github-auth'}
      className={cn('flex gap-2', buttonVariants({ ...props }), className)}
    >
      {user ? (
        <>
          <DashboardIcon /> Dashboard
        </>
      ) : (
        <>
          <SignInIcon /> Sign in
        </>
      )}
    </a>
  )
}
