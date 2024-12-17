'use server'

import Link from 'next/link'
import type { FC } from 'react'
import { buttonVariants, type ButtonProps } from '~/shared/components/button'
import { SignInIcon } from '~/shared/icons'
import { authenticate } from '~/shared/lib/auth/authenticate'
import { cn } from '~/shared/lib/classnames'

type Props = ButtonProps

export const SignIn: FC<Props> = async ({ className, ...props }) => {
  const user = await authenticate()

  return user ? (
    <Link href="/dashboard">Dashboard</Link>
  ) : (
    <Link href="/api/github-auth" className={cn('flex gap-2', buttonVariants({ ...props }), className)}>
      <SignInIcon /> Sign in
    </Link>
  )
}
