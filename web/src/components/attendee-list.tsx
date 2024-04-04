import { ChangeEvent, useEffect, useState } from 'react'
import {
  ChevronLeft,
  ChevronRight,
  ChevronsLeft,
  ChevronsRight,
  MoreHorizontal,
  Search,
} from 'lucide-react'
import dayjs from 'dayjs'
import 'dayjs/locale/pt-br'
import relativeTime from 'dayjs/plugin/relativeTime'
import { IconButton } from './icon-button'
import { Table } from './table/table'
import { TableHeader } from './table/table-header'
import { TableCell } from './table/table-cell'
import { TableRow } from './table/table-row'
import { Attendee } from '../models/attendee'

dayjs.extend(relativeTime)
dayjs.locale('pt-br')

export function AttendeeList() {
  const [search, setSearch] = useState(() => {
    const url = new URL(window.location.toString())
    if (url.searchParams.has('search')) {
      return url.searchParams.get('search') ?? ''
    }
    return ''
  })
  const [page, setPage] = useState(() => {
    const url = new URL(window.location.toString())
    if (url.searchParams.has('page')) {
      return Number(url.searchParams.get('page') ?? '1')
    }
    return 1
  })
  const [attendees, setAttendees] = useState<Attendee[]>([])
  const [total, setTotal] = useState(0)

  const totalPages = Math.ceil(total / 10)

  function setCurrentSearch(search: string) {
    const url = new URL(window.location.toString())
    url.searchParams.set('search', search)
    window.history.pushState({}, '', url)
    setSearch(search)
  }

  function setCurrentPage(page: number) {
    const url = new URL(window.location.toString())
    url.searchParams.set('page', String(page))
    window.history.pushState({}, '', url)
    setPage(page)
  }

  function onSearchInputChanged(event: ChangeEvent<HTMLInputElement>) {
    setCurrentSearch(event.target.value)
    setCurrentPage(1)
  }

  function goToFirstPage() {
    setCurrentPage(1)
  }

  function goToPreviousPage() {
    setCurrentPage(page - 1)
  }

  function goToNextPage() {
    setCurrentPage(page + 1)
  }

  function goToLastPage() {
    setCurrentPage(totalPages)
  }

  useEffect(() => {
    const eventId = '9e9bd979-9d10-4915-b339-3786b1634f33'
    const url = new URL(`http://localhost:3333/events/${eventId}/attendees`)

    url.searchParams.set('pageIndex', String(page - 1))
    if (search.trim().length > 0) {
      url.searchParams.set('query', search)
    }

    fetch(url)
      .then((response) => response.json())
      .then((data) => {
        setAttendees(data.attendees)
        setTotal(data.total)
      })
  }, [page, search])

  return (
    <div className="flex flex-col gap-4">
      <div className="flex gap-3 items-center">
        <h1 className="text-2xl font-bold">Participantes</h1>

        <div className="px-3 w-72 py-1.5 border border-white/10 rounded-lg flex items-center gap-3">
          <Search className="size-4 text-emerald-300" />

          <input
            className="bg-transparent flex-1 outline-none ring-0 border-0 p-0 text-sm focus:ring-0"
            placeholder="Buscar participante..."
            value={search}
            onChange={onSearchInputChanged}
          />
        </div>
      </div>

      <Table className="w-full">
        <thead>
          <tr className="border-b border-white/10">
            <TableHeader style={{ width: 48 }}>
              <input
                type="checkbox"
                className="size-4 bg-black/20 rounded border-white/10 text-orange-400"
              />
            </TableHeader>

            <TableHeader>Código</TableHeader>

            <TableHeader>Participante</TableHeader>

            <TableHeader>Data de inscrição</TableHeader>

            <TableHeader>Data do check-in</TableHeader>

            <TableHeader style={{ width: 64 }}></TableHeader>
          </tr>
        </thead>

        <tbody>
          {attendees.map((attendee) => (
            <TableRow key={attendee.id}>
              <TableCell>
                <input
                  type="checkbox"
                  className="size-4 bg-black/20 rounded border-white/10 text-orange-400"
                />
              </TableCell>

              <TableCell>{attendee.id}</TableCell>

              <TableCell>
                <div className="flex flex-col gap-1">
                  <span className="font-semibold text-white">
                    {attendee.name}
                  </span>

                  <span>{attendee.email}</span>
                </div>
              </TableCell>

              <TableCell>{dayjs().to(attendee.createdAt)}</TableCell>

              <TableCell>
                {attendee.checkedInAt === null ? (
                  <span className="text-zinc-400">não fez check-in</span>
                ) : (
                  dayjs().to(attendee.checkedInAt)
                )}
              </TableCell>

              <TableCell>
                <IconButton transparent>
                  <MoreHorizontal className="size-4" />
                </IconButton>
              </TableCell>
            </TableRow>
          ))}
        </tbody>

        <tfoot>
          <tr>
            <TableCell
              colSpan={3}
            >{`Mostrando ${attendees.length} de ${total} items`}</TableCell>

            <TableCell colSpan={3} className="text-right">
              <div className="inline-flex items-center gap-8">
                <span>{`Página ${page} de ${totalPages}`}</span>

                <div className="flex gap-1.5">
                  <IconButton disabled={page === 1} onClick={goToFirstPage}>
                    <ChevronsLeft className="size-4" />
                  </IconButton>

                  <IconButton disabled={page === 1} onClick={goToPreviousPage}>
                    <ChevronLeft className="size-4" />
                  </IconButton>

                  <IconButton
                    disabled={page === totalPages}
                    onClick={goToNextPage}
                  >
                    <ChevronRight className="size-4" />
                  </IconButton>

                  <IconButton
                    disabled={page === totalPages}
                    onClick={goToLastPage}
                  >
                    <ChevronsRight className="size-4" />
                  </IconButton>
                </div>
              </div>
            </TableCell>
          </tr>
        </tfoot>
      </Table>
    </div>
  )
}
