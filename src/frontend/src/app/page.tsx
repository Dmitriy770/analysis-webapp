import { Text } from '~/shared/components/text'
import { LandingHeader } from './landing-header'

export default function Home() {
  return (
    <div className="h-[100dvh]">
      <LandingHeader />
      <div className="-translate-x-1/2 -translate-y-1/2 absolute top-1/2 left-1/2 scale-125">
        <Text variant="heading-xl">analysis-webapp</Text>
      </div>
    </div>
  )
}
