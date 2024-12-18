import type { Metadata } from 'next'
import { JetBrains_Mono, Onest } from 'next/font/google'
import './(styles)/index.css'
import { ThemeProvider } from 'next-themes'
import { cn } from '~/shared/lib/classnames'
import { TooltipProvider } from '~/shared/components/tooltip'

export const metadata: Metadata = {
  title: 'analysis-webapp',
}

const normalFont = Onest({
  subsets: ['latin', 'cyrillic'],
  variable: '--font-normal',
})

const monoFont = JetBrains_Mono({
  subsets: ['latin', 'cyrillic'],
  variable: '--font-mono',
})

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode
}>) {
  return (
    <html lang="ru" suppressHydrationWarning>
      <body className={cn(normalFont.variable, monoFont.variable, 'antialiased')}>
        <ThemeProvider attribute="class" defaultTheme="dark" enableSystem>
          <TooltipProvider>{children}</TooltipProvider>
        </ThemeProvider>
      </body>
    </html>
  )
}
