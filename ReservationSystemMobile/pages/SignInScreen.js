import { useState, useContext } from 'react'
import { View, Text, Button, TextInput, Pressable } from 'react-native'
import { AuthContext, useAuthContext } from '../components'

export function SignInScreen({ navigation }) {
    const [email, setEmail] = useState('')
    const [password, setPassword] = useState('')
    const [error, setError] = useState('')

    const {
        authState,
        dispatch,
        actions: { signIn, signUp, signOut },
    } = useAuthContext()

    async function handleSignIn() {
        try {
            await signIn({ email, password })(dispatch)
        } catch (e) {
            console.log(e)
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
