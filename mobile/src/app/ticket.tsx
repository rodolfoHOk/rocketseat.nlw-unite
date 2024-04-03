import { useState } from 'react'
import {
  Alert,
  Modal,
  ScrollView,
  Share,
  StatusBar,
  Text,
  TouchableOpacity,
  View,
} from 'react-native'
import { FontAwesome } from '@expo/vector-icons'
import * as ImagePicker from 'expo-image-picker'
import { MotiView } from 'moti'
import { Header } from '@/components/header'
import { Credential } from '@/components/credential'
import { Button } from '@/components/button'
import { colors } from '@/styles/colors'
import { QRCode } from '@/components/qrcode'
import { useBadgeStore } from '@/store/badge-store'
import { Redirect } from 'expo-router'

export default function Ticket() {
  const [expandeQRCode, setExpandeQRCode] = useState(false)

  const badgeStore = useBadgeStore()

  async function handleSelectImage() {
    try {
      const result = await ImagePicker.launchImageLibraryAsync({
        mediaTypes: ImagePicker.MediaTypeOptions.Images,
        allowsEditing: true,
        aspect: [4, 4],
      })

      if (result.assets) {
        badgeStore.updateAvatar(result.assets[0].uri)
      }
    } catch (error) {
      console.error(error)
      Alert.alert('Foto', 'Não foi possível selecionar a imagem!')
    }
  }

  async function handleShare() {
    try {
      if (badgeStore.data?.checkInURL) {
        await Share.share({
          message: badgeStore.data.checkInURL,
        })
      }
    } catch (error) {
      console.log(error)
      Alert.alert('Compartilhar', 'Não foi possível compartilhar')
    }
  }

  if (!badgeStore.data?.checkInURL) {
    return <Redirect href="/" />
  }

  return (
    <View className="flex-1 bg-green-500">
      <StatusBar barStyle="light-content" />

      <Header title="Minha Credencial" />

      <ScrollView
        className="-mt-28 -z-10"
        contentContainerClassName="px-8 pb-8"
        showsVerticalScrollIndicator={false}
      >
        <Credential
          data={badgeStore.data}
          onChangeAvatar={handleSelectImage}
          onExpandQRCode={() => setExpandeQRCode(true)}
        />

        <MotiView
          from={{
            translateY: 0,
          }}
          animate={{
            translateY: 10,
          }}
          transition={{
            loop: true,
            type: 'timing',
            duration: 700,
          }}
        >
          <FontAwesome
            name="angle-double-down"
            size={24}
            color={colors.gray[300]}
            className="self-center my-6"
          />
        </MotiView>

        <Text className="text-white font-bold text-2xl mt-4">
          Compartilhar credencial
        </Text>

        <Text className="text-white font-regular text-base mt-1 mb-6">
          {`Mostre ao mundo que você vai participar do evento ${badgeStore.data.eventTitle}!`}
        </Text>

        <Button title="Compartilhar" onPress={handleShare} />

        <TouchableOpacity
          activeOpacity={0.7}
          className="mt-10"
          onPress={() => badgeStore.remove()}
        >
          <Text className="text-base text-white font-bold text-center">
            Remover Ingresso
          </Text>
        </TouchableOpacity>
      </ScrollView>

      <Modal visible={expandeQRCode} statusBarTranslucent animationType="fade">
        <View className="flex-1 bg-green-500 items-center justify-center">
          <TouchableOpacity
            activeOpacity={0.7}
            onPress={() => setExpandeQRCode(false)}
          >
            <QRCode value={'teste'} size={300} />

            <Text className="font-body text-sm text-orange-500 text-center mt-10">
              Fechar
            </Text>
          </TouchableOpacity>
        </View>
      </Modal>
    </View>
  )
}
