import { Image, View } from 'react-native'
import { MaterialCommunityIcons } from '@expo/vector-icons'

import { colors } from '@/styles/colors'
import { Input } from '@/components/input'
import { Button } from '@/components/button'

export default function Home() {
  return (
    <View className="flex-1 items-center justify-center p-8 bg-green-500">
      <Image
        source={require('@/assets/logo.png')}
        className="h-16"
        resizeMode="contain"
      />

      <View className="w-full mt-12 gap-3">
        <Input>
          <MaterialCommunityIcons
            name="ticket-confirmation-outline"
            color={colors.green[200]}
            size={20}
          />

          <Input.Field placeholder="Código do ingresso" />
        </Input>

        <Button
          title="Acessar credencial"
          isLoading={false}
          onPress={() => console.warn('CLICOU')}
        />
      </View>
    </View>
  )
}
