import { Header } from '@/components/header'
import { Credential } from '@/components/credential'
import { StatusBar, View } from 'react-native'

export default function Ticket() {
  return (
    <View className="flex-1 bg-green-500">
      <StatusBar barStyle="light-content" />

      <Header title="Minha Credencial" />

      <Credential />
    </View>
  )
}
