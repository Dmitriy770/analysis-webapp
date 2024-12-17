import type { FC } from 'react'
import { Text } from '~/shared/components/text'
import { authenticate } from '~/shared/lib/auth/authenticate'

const DashboardPage: FC = async () => {
  const user = await authenticate()

  return <Text color={user ? 'positive' : 'negative'}>{user?.nickname ?? ':('}</Text>
}

export default DashboardPage
