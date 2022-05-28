import { useState, useContext } from 'react'
import { View, Text, Button, TextInput, Pressable } from 'react-native'
import { AuthContext } from '../App'

export function SignInScreen({ navigation }) {
    const [email, setEmail] = useState('')
    const [password, setPassword] = useState('')
    const [error, setError] = useState('')

    const { signIn } = useContext(AuthContext)

    async function handleSignIn() {
        try {
            await signIn({ email, password })
        } catch (e) {
            setError(e.message)
        }
    }

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
            <Button title="Sign In" onPress={() => handleSignIn()} />
            <Text>{error}</Text>
        </View>
    )
}
