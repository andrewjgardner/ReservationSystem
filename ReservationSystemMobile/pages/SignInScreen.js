import { useState, useContext } from 'react'
import { View, Text, Button, TextInput, Pressable, StyleSheet } from 'react-native'
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
        <View style={styles.container}>
            <Text>Email</Text>
            <TextInput
                value={email}
                onChangeText={setEmail}
                keyboardType="email-address"
                style={styles.input}
            />
            <Text>Password</Text>
            <TextInput
                value={password}
                onChangeText={setPassword}
                secureTextEntry={true}
                style={styles.input}
            />
            <Button title="Sign In" onPress={() => handleSignIn()} />
            <Text>{error}</Text>
        </View>
    )
}

const styles = StyleSheet.create({
    container: {
        flex: 1,
        justifyContent: 'center',
        alignItems: 'center',
    },
    input: {
        borderWidth: 1,
        borderColor: '#ccc',
        padding: 10,
        margin: 10,
        width: 200,
        backgroundColor: '#d3d1d1'
    },
}) 
