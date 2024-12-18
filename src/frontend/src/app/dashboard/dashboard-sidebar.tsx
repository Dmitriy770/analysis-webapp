'use client'

import Link from 'next/link'
import { type ComponentProps, type FC, type PropsWithChildren, type ReactNode, Suspense } from 'react'
import { buttonVariants } from '~/shared/components/button'
import { Separator } from '~/shared/components/separator'
import { ThemeToggle } from '~/shared/components/theme-toggle'
import { Tooltip, TooltipArrow, TooltipContent, TooltipTrigger } from '~/shared/components/tooltip'
import { HomeIcon, SignOutIcon } from '~/shared/icons'
import { cn } from '~/shared/lib/classnames'
import { UserAvatar } from './user-avatar'
import type { User } from '~/shared/lib/api'

type Props = ComponentProps<'aside'> & { user: User }

export const DashboardSidebar: FC<Props> = props => {
  const { className, user, ...asideProps } = props

  return (
    <aside
      {...asideProps}
      className={cn('flex h-full w-min max-w-[300px] flex-col gap-2 border-border border-r p-2', className)}
    >
      <SidebarLink href="/" icon={<HomeIcon />}>
        Landing
      </SidebarLink>
      <Separator />
      <div className="flex-grow" />
      <Suspense>
        <AccountSidebarLink user={user} />
      </Suspense>
      <ThemeToggle variant="ghost" />
    </aside>
  )
}

const SidebarLink: FC<
  PropsWithChildren & {
    href: string
    icon: ReactNode
    className?: string
  }
> = props => {
  const { href, icon, className, children } = props

  return (
    <Tooltip delayDuration={0}>
      <TooltipTrigger asChild>
        <Link href={href} className={buttonVariants({ variant: 'ghost' })}>
          {icon}
        </Link>
      </TooltipTrigger>
      {children ? (
        <TooltipContent side="right" sideOffset={0} className={className}>
          {children}
        </TooltipContent>
      ) : null}
      <TooltipArrow />
    </Tooltip>
  )
}

const AccountSidebarLink: FC<{ user: User }> = ({ user }) => {
  return (
    <SidebarLink
      href={`https://github.com/${user.nickname}`}
      icon={<UserAvatar user={user} className="h-[36px] w-[36px]" />}
      className="flex flex-row items-center gap-2 pr-1"
    >
      <span>{user.nickname}</span>
      <Link href="/sign-out" prefetch={false} className={cn(buttonVariants({ variant: 'ghost' }), 'h-min p-1')}>
        <SignOutIcon />
      </Link>
    </SidebarLink>
  )
}
