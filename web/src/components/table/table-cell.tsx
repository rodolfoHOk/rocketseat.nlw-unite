import { ComponentProps } from 'react'

interface TableCellProps extends ComponentProps<'td'> {}

export function TableCell({ className, ...rest }: TableCellProps) {
  return (
    <td className={`py-3 px-4 text-sm text-zinc-300" ${className}`} {...rest} />
  )
}
