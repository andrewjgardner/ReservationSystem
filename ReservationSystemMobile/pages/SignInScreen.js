import { useState, useContext } from 'react'
import {
    View,
    Text,
    Button,
    TextInput,
    Pressable,
    StyleSheet,
    Image,
} from 'react-native'
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
            <View style={styles.logoContainer}>
                <Image
                    style={styles.logo}
                    source={require('../assets/bean-scene-logo.png')}
                />
                <Text style={styles.headerText}>Bean Scene</Text>
            </View>
            <View style={styles.inputContainer}>
                <TextInput
                    value={email}
                    onChangeText={setEmail}
                    keyboardType="email-address"
                    style={styles.input}
                    placeholder="Email"
                />
                <TextInput
                    value={password}
                    onChangeText={setPassword}
                    secureTextEntry={true}
                    style={styles.input}
                    placeholder="Password"
                />
                <Pressable onPress={handleSignIn} style={styles.button}>
                    <Text style={styles.buttonText}>Sign In</Text>
                </Pressable>
                <Text style={styles.errorText}>{error}</Text>
            </View>
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
        backgroundColor: '#d3d1d1',
    },
    headerText: {
        color: '#0d6efd',
        fontSize: 32,
        fontWeight: 'bold',
        marginBottom: 30,
    },
    logo: {
        height: 100,
        width: 200,
        resizeMode: 'contain',
        marginBottom: 20,
    },
    inputContainer: {
        justifyContent: 'center',
        alignItems: 'center',
    },
    logoContainer: {
        justifyContent: 'center',
        alignItems: 'center',
    },
    button: {
        alignItems: 'center',
        justifyContent: 'center',
        paddingVertical: 12,
        paddingHorizontal: 32,
        borderRadius: 4,
        elevation: 3,
        backgroundColor: '#0d6efd',
        marginVertical: 10,
    },
    buttonText: {
        color: 'white',
        fontWeight: 'bold',
        fontSize: 16,
    },
    errorText: {
        color: 'red',
        fontSize: 16,
    },
})
