import '@/styles/global.css'

import {
  useFonts,
  Roboto_400Regular,
  Roboto_500Medium,
  Roboto_700Bold,
} from '@expo-google-fonts/roboto'
import { Slot } from 'expo-router'
import { StatusBar } from 'expo-status-bar'

import { Loading } from '@/components/loading'

export default function Layout() {
  const [fontsLoaded] = useFonts({
    Roboto_400Regular,
    Roboto_500Medium,
    Roboto_700Bold,
  })

  return (
    <>
      <StatusBar style="light" />
      {fontsLoaded ? <Slot /> : <Loading />}
    </>
  )
}
