import type { ComponentProps, FC } from 'react'
import { cn } from '~/shared/lib/classnames'

type Props = ComponentProps<'div'> & {
  width: number
  height: number
}

export const Skeleton: FC<Props> = ({ className, width, height, style, ...props }) => {
  return (
    <div
      {...props}
      className={cn('animate-pulse rounded-md bg-muted', className)}
      style={{ ...style, width, height }}
    />
  )
}
