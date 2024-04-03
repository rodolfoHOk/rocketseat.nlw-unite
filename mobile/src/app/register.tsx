import { Alert, Image, StatusBar, View } from 'react-native'
import { FontAwesome6, MaterialIcons } from '@expo/vector-icons'
import { Link, router } from 'expo-router'

import { Input } from '@/components/input'
import { Button } from '@/components/button'
import { colors } from '@/styles/colors'
import { useState } from 'react'
import { api } from '@/server/api'
import axios from 'axios'

const EVENT_ID = '9e9bd979-9d10-4915-b339-3786b1634f33'

export default function Register() {
  const [name, setName] = useState('')
  const [email, setEmail] = useState('')
  const [isLoading, setIsLoading] = useState(false)

  async function handleRegister() {
    try {
      if (!name.trim()) {
        return Alert.alert('Inscrição', 'Informe seu nome completo!')
      }
      if (!email.trim()) {
        return Alert.alert('Inscrição', 'Informe seu e-mail!')
      }

      setIsLoading(true)

      const registerResponse = await api.post(`/events/${EVENT_ID}/attendees`, {
        name,
        email,
      })
      if (registerResponse.data.attendeeId) {
        Alert.alert('Inscrição', 'Inscrição realizada com sucesso!', [
          {
            text: 'Ok',
            onPress: () => router.push('/ticket'),
          },
        ])
      } else {
        setIsLoading(false)
      }
    } catch (error) {
      console.log(error)
      setIsLoading(false)

      if (axios.isAxiosError(error)) {
        if (
          String(error.response?.data.message).includes('already registered')
        ) {
          Alert.alert('Inscrição', 'Este e-mail já está cadastrado')
        } else {
          Alert.alert('Inscrição', 'Não foi possível fazer a inscrição')
        }
      } else {
        Alert.alert('Inscrição', 'Não foi possível fazer a inscrição')
      }
    }
  }

  return (
    <View className="flex-1 items-center justify-center p-8 bg-green-500">
      <StatusBar barStyle="light-content" />

      <Image
        source={require('@/assets/logo.png')}
        className="h-16"
        resizeMode="contain"
      />

      <View className="w-full mt-12 gap-3">
        <Input>
          <FontAwesome6
            name="user-circle"
            color={colors.green[200]}
            size={20}
          />

          <Input.Field placeholder="Nome completo" onChangeText={setName} />
        </Input>

        <Input>
          <MaterialIcons
            name="alternate-email"
            color={colors.green[200]}
            size={20}
          />

          <Input.Field
            placeholder="E-mail"
            keyboardType="email-address"
            onChangeText={setEmail}
          />
        </Input>

        <Button
          title="Realizar inscrição"
          isLoading={isLoading}
          onPress={handleRegister}
        />

        <Link
          href="/"
          className="text-gray-100 text-base font-bold text-center mt-8"
        >
          Já possui ingresso?
        </Link>
      </View>
    </View>
  )
}
