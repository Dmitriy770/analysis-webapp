import type { FC } from 'react'
import { Avatar, AvatarFallback, AvatarImage, type AvatarProps } from '~/shared/components/avatar'
import type { User } from '~/shared/lib/api'

type Props = AvatarProps & {
  user: User
}

export const UserAvatar: FC<Props> = props => {
  const { user, ...avatarProps } = props

  return (
    <Avatar {...avatarProps}>
      <AvatarImage src={user.avatarUrl} />
      <AvatarFallback>
        {user.nickname
          .split(' ', 2)
          .map(p => p[0].toUpperCase())
          .join('')}
      </AvatarFallback>
    </Avatar>
  )
}
