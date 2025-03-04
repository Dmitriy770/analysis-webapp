import { Slot } from '@radix-ui/react-slot'
import { type VariantProps, cva } from 'class-variance-authority'
import { type ComponentProps, forwardRef } from 'react'
import { cn } from '~/shared/lib/classnames'

const buttonVariants = cva(
  'relative inline-flex items-center justify-center whitespace-nowrap rounded-md font-medium text-sm ring-offset-background transition-colors focus-visible:outline-none focus-visible:ring-2 focus-visible:ring-ring focus-visible:ring-offset-2 disabled:pointer-events-none disabled:opacity-50',
  {
    variants: {
      variant: {
        primary: 'bg-primary text-primary-foreground hover:bg-primary/90',
        destructive: 'bg-destructive text-destructive-foreground hover:bg-destructive/90',
        outline: 'border border-input bg-background hover:bg-accent hover:text-accent-foreground',
        secondary: 'bg-secondary text-secondary-foreground hover:bg-secondary/80',
        ghost: 'hover:bg-accent hover:text-accent-foreground',
        link: 'text-primary underline-offset-4 hover:underline',
      },
      size: {
        sm: 'h-8 rounded-md px-3',
        md: 'h-10 px-4 py-2',
        lg: 'h-12 rounded-md px-8',
      },
      shape: {
        pill: '',
        square: '',
      },
    },
    compoundVariants: [
      {
        size: 'sm',
        shape: 'square',
        className: 'p-4',
      },
      {
        size: 'md',
        shape: 'square',
        className: 'p-5',
      },
      {
        size: 'lg',
        shape: 'square',
        className: 'p-6',
      },
    ],
    defaultVariants: {
      variant: 'primary',
      size: 'md',
      shape: 'pill',
    },
  },
)

type ButtonProps = ComponentProps<'button'> &
  VariantProps<typeof buttonVariants> & {
    asChild?: boolean
  }

type ButtonVariant = NonNullable<ButtonProps['variant']>

type ButtonSize = NonNullable<ButtonProps['size']>

type ButtonShape = NonNullable<ButtonProps['shape']>

const Button = forwardRef<HTMLButtonElement, ButtonProps>(
  ({ className, variant, size, shape, asChild = false, ...props }, ref) => {
    const Component = asChild ? Slot : 'button'
    return <Component {...props} ref={ref} className={cn(buttonVariants({ variant, size, shape }), className)} />
  },
)

Button.displayName = 'Button'

export { Button, buttonVariants, type ButtonProps, type ButtonVariant, type ButtonSize, type ButtonShape }
