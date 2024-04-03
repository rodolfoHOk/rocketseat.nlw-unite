import AsyncStorage from '@react-native-async-storage/async-storage'
import { create } from 'zustand'
import { createJSONStorage, persist } from 'zustand/middleware'

export type BadgeStore = {
  id: string
  name: string
  email: string
  eventTitle: string
  checkInURL: string
  image?: string
}

type StateProps = {
  data: BadgeStore | null
  save: (data: BadgeStore) => void
}

export const useBadgeStore = create(
  persist<StateProps>(
    (set) => ({
      data: null,
      save: (data: BadgeStore) => set(() => ({ data })),
    }),
    {
      name: 'nlw-unite:badge',
      storage: createJSONStorage(() => AsyncStorage),
    }
  )
)
