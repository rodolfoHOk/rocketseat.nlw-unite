import { ComponentProps } from 'react'

interface IconButtonProps extends ComponentProps<'button'> {
  transparent?: boolean
}

export function IconButton({
  children,
  className,
  disabled,
  transparent,
  ...rest
}: IconButtonProps) {
  return (
    <button
      className={`border border-white/10 rounded-md p-1.5 transition-colors duration-200
        ${transparent ? 'bg-black/20' : 'bg-white/10'}
        ${disabled ? 'opacity-50' : 'hover:bg-white/20'} ${className}`}
      {...rest}
      disabled={disabled}
    >
      {children}
    </button>
  )
}
