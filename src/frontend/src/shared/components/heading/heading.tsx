import { type ComponentProps, forwardRef } from 'react'
import { cn } from '~/shared/lib/classnames'
import { createSlot, useSlots } from '~/shared/lib/slots'
import { Text, type TextProps } from '../text'

type SuperTitleProps = TextProps
type TitleProps = TextProps
type DescriptionProps = TextProps

const SuperTitleSlot = createSlot<SuperTitleProps>('SuperTitle')
const TitleSlot = createSlot<TitleProps>('Title')
const DescriptionSlot = createSlot<DescriptionProps>('Description')

type RootProps = ComponentProps<'div'>

const Root = forwardRef<HTMLDivElement, RootProps>((props, ref) => {
  const { children, className, ...rootProps } = props

  const slots = useSlots({ children, defaultSlot: TitleSlot })

  const superTitle = slots.get(SuperTitleSlot)
  const title = slots.get(TitleSlot)
  const description = slots.get(DescriptionSlot)

  if (!superTitle && !title && !description) {
    return null
  }

  return (
    <div {...rootProps} ref={ref} className={cn('flex flex-col', className)}>
      {superTitle && (
        <Text variant="text-xs" color="secondary" {...superTitle.props} ref={superTitle.ref}>
          {superTitle.children}
        </Text>
      )}
      {title && (
        <Text variant="heading-m" color="primary" {...title.props} ref={title.ref}>
          {title.children}
        </Text>
      )}
      {description && (
        <Text variant="text-m" color="primary" {...description.props} ref={description.ref}>
          {description.children}
        </Text>
      )}
    </div>
  )
})

Root.displayName = 'Heading'

export {
  Root as Heading,
  SuperTitleSlot as HeadingSuperTitle,
  TitleSlot as HeadingTitle,
  DescriptionSlot as HeadingDescription,
  type RootProps as HeadingProps,
  type SuperTitleProps as HeaderSuperTitleProps,
  type TitleProps as HeaderTitleProps,
  type DescriptionProps as HeaderDescriptionProps,
}
