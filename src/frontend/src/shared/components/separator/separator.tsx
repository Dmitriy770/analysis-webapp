import * as SeparatorPrimitive from '@radix-ui/react-separator'
import { type ComponentProps, forwardRef } from 'react'
import { cn } from '~/shared/lib/classnames'

type Props = ComponentProps<'div'> & {
  orientation?: 'vertical' | 'horizontal'
  decorative?: boolean
}

const Separator = forwardRef<HTMLDivElement, Props>(
  ({ className, orientation = 'horizontal', decorative = true, ...props }, ref) => (
    <SeparatorPrimitive.Root
      ref={ref}
      decorative={decorative}
      orientation={orientation}
      className={cn(
        'shrink-0 bg-border',
        orientation === 'horizontal' ? 'h-[1px] w-full' : 'h-full w-[1px]',
        className,
      )}
      {...props}
    />
  ),
)

Separator.displayName = 'Separator'

export { Separator, type Props }
