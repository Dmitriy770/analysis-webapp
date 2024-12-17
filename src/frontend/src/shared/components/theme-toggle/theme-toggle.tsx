'use client'

import { MoonIcon, SunIcon } from '~/shared/icons'
import { useTheme } from 'next-themes'
import { cn } from '~/shared/lib/classnames'
import { Button, type ButtonProps } from '~/shared/components/button'
import { useCallback, type FC } from 'react'

type Props = Omit<ButtonProps, 'children' | 'onClick'> & Readonly<{}>

export const ThemeToggle: FC<Props> = ({ className, ...buttonProps }) => {
  const { setTheme } = useTheme()

  const toggleTheme = useCallback(() => {
    setTheme(theme => (theme === 'light' ? 'dark' : 'light'))
  }, [setTheme])

  return (
    <Button {...buttonProps} className={cn('space-x-2', className)} onClick={toggleTheme}>
      <span className="relative h-[24px] w-[24px]">
        <SunIcon className="absolute rotate-0 scale-100 transition-all dark:rotate-90 dark:scale-0" />
        <MoonIcon className="absolute rotate-90 scale-0 transition-all dark:rotate-0 dark:scale-100" />
      </span>
    </Button>
  )
}
