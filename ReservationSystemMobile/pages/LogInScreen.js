import { useState } from 'react'
import { View, Text, Button, TextInput, Pressable } from 'react-native'

export function LogInScreen({ navigation }) {
    const [email, setEmail] = useState('')
    const [password, setPassword] = useState('')

    return (
        <View>
            <Text>Email</Text>
            <TextInput
                value={email}
                onChangeText={setEmail}
                keyboardType="email-address"
            />
            <Text>Password</Text>
            <TextInput
                value={password}
                onChangeText={setPassword}
                secureTextEntry={true}
            />
            <Button title='Log In' />
        </View>
    )
}
