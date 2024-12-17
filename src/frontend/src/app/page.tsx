import { Button } from '~/shared/components/button'
import { Heading } from '~/shared/components/heading'
import { Skeleton } from '~/shared/components/skeleton'
import { Text } from '~/shared/components/text'
import { ThemeToggle } from '~/shared/components/theme-toggle'

export default function Home() {
  return (
    <div>
      <Heading>
        <Heading.Title>Добро пожаловать!</Heading.Title>
        <Heading.Description className="italic" color="negative">
          Тут будет кластеризация
        </Heading.Description>
      </Heading>
      <Button>
        <Text variant="text-l">Hello, world!</Text>
      </Button>
      <ThemeToggle />
      <Skeleton width={100} height={100} />
    </div>
  )
}
