import { ComponentProps } from 'react'

interface NavLinkProps extends ComponentProps<'a'> {
  children: string
}

export function NavLink({ children, className, ...rest }: NavLinkProps) {
  return (
    <a className={`font-medium text-sm text-zinc-300 ${className}`} {...rest}>
      {children}
    </a>
  )
}
