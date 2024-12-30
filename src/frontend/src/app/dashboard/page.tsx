import type { FC } from 'react'
import { DashboardSidebar } from './dashboard-sidebar'
import { authenticate } from '~/shared/lib/auth/authenticate'
import { redirect, RedirectType } from 'next/navigation'

const DashboardPage: FC = async () => {
  const user = await authenticate()

  if (!user) {
    redirect('/', RedirectType.replace)
  }

  return (
    <div className="h-[100dvh]">
      <DashboardSidebar user={user} />
    </div>
  )
}

export default DashboardPage
