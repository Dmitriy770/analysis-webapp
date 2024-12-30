import * as AvatarPrimitive from '@radix-ui/react-avatar'
import { type ComponentProps, forwardRef } from 'react'
import { cn } from '~/shared/lib/classnames'
import { createSlot, useSlots } from '~/shared/lib/slots'

type RootProps = ComponentProps<'span'>
type ImageProps = Omit<ComponentProps<'img'>, 'children'>
type FallbackProps = ComponentProps<'span'>

const ImageSlot = createSlot<ImageProps>('Image')
const FallbackSlot = createSlot<FallbackProps>('Fallback')

const Root = forwardRef<HTMLSpanElement, RootProps>((props, ref) => {
  const { className, children, ...rootProps } = props

  const slots = useSlots({ children })

  const image = slots.get(ImageSlot)
  const fallback = slots.get(FallbackSlot)

  if (!image && !fallback) {
    return null
  }

  return (
    <AvatarPrimitive.Root
      {...rootProps}
      ref={ref}
      className={cn('relative flex h-10 w-10 shrink-0 overflow-hidden rounded-full', className)}
    >
      {image && (
        <AvatarPrimitive.Image
          {...image.props}
          ref={image.ref}
          className={cn('aspect-square h-full w-full', image.props.className)}
        />
      )}
      {fallback && (
        <AvatarPrimitive.Fallback
          {...fallback.props}
          ref={fallback.ref}
          className={cn(
            'flex h-full w-full items-center justify-center rounded-full bg-muted',
            fallback.props.className,
          )}
        >
          {fallback.children}
        </AvatarPrimitive.Fallback>
      )}
    </AvatarPrimitive.Root>
  )
})

Root.displayName = 'Avatar'

export {
  Root as Avatar,
  ImageSlot as AvatarImage,
  FallbackSlot as AvatarFallback,
  type RootProps as AvatarProps,
  type ImageProps as AvatarImageProps,
  type FallbackProps as AvatarFallbackProps,
}
