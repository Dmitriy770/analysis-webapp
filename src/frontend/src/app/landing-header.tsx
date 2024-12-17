import { ThemeToggle } from '~/shared/components/theme-toggle'
import { SignIn } from './sign-in'

export const LandingHeader = () => {
  return (
    <div className="flex h-16 w-full flex-row items-center border-b border-b-border px-2">
      <SignIn size="lg" variant="outline" shape="square" />
      <div className="flex-grow" />
      <ThemeToggle size="lg" variant="outline" />
    </div>
  )
}
