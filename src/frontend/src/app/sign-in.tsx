import Link from 'next/link'
import type { FC } from 'react'
import { buttonVariants, type ButtonProps } from '~/shared/components/button'
import { SignInIcon } from '~/shared/icons'
import { cn } from '~/shared/lib/classnames'

type Props = ButtonProps

export const SignIn: FC<Props> = ({ className, ...props }) => {
  return (
    <Link href="/" className={cn('flex gap-2', buttonVariants({ ...props }), className)}>
      <SignInIcon /> Sign in
    </Link>
  )
}
