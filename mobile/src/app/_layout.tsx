import '../styles/global.css'

import { Slot } from 'expo-router'
import { StatusBar } from 'expo-status-bar'
import {
  useFonts,
  Roboto_400Regular,
  Roboto_500Medium,
  Roboto_700Bold,
} from '@expo-google-fonts/roboto'

export default function Layout() {
  const [fontsLoaded] = useFonts({
    Roboto_400Regular,
    Roboto_500Medium,
    Roboto_700Bold,
  })

  if (!fontsLoaded) {
    return
  }

  return (
    <>
      <StatusBar style="light" />
      <Slot />
    </>
  )
}
