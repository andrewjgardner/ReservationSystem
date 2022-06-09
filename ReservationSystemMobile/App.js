import 'react-native-gesture-handler'
import { StatusBar } from 'expo-status-bar'
import { StyleSheet, Text, View } from 'react-native'
import AsyncStorage from '@react-native-async-storage/async-storage'
import { NavigationContainer, StackActions } from '@react-navigation/native'
import { createNativeStackNavigator } from '@react-navigation/native-stack'
import { HomeScreen, SignInScreen, SplashScreen } from './pages'
import { apiFetch, getLoggedInUser } from './services/FetchService'
import {
    useState,
    useContext,
    useReducer,
    useEffect,
    createContext,
    useMemo,
} from 'react'
import { DrawerNavigator } from './components/DrawerNavigator'

const Stack = createNativeStackNavigator()
export const AuthContext = createContext()

export default function App() {
    const [authState, dispatch] = useReducer(
        (prevState, action) => {
            switch (action.type) {
                case 'RESTORE_TOKEN':
                    return {
                        ...prevState,
                        userToken: action.token,
                        isLoading: false,
                    }
                case 'SIGN_IN':
                    return {
                        ...prevState,
                        isSignOut: false,
                        userToken: action.token,
                    }
                case 'SIGN_OUT':
                    return {
                        ...prevState,
                        isSignOut: true,
                        userToken: null,
                    }
                case 'STATE':
                    return prevState
            }
        },
        {
            isLoading: true,
            isSignOut: false,
            userToken: null,
        }
    )

    useEffect(() => {
        const bootstrapAsync = async () => {
            let userToken
            try {
                userToken = await AsyncStorage.getItem('userToken')
            } catch (e) {}
            dispatch({ type: 'RESTORE_TOKEN', token: userToken })

            try {
                let userDetails = await getLoggedInUser()
            } catch (e) {
                dispatch({ type: 'SIGN_OUT' })
            }
        }
        bootstrapAsync()
    }, [])

    const authContext = useMemo(
        () => ({
            signIn: async (data) => {
                const response = await apiFetch('token', 'POST', {
                    email: data.email,
                    password: data.password,
                })
                if (response.status !== 200) {
                    throw new Error("Couldn't sign in")
                }
                const jwt = await response.text()

                await AsyncStorage.setItem('userToken', jwt)
                dispatch({ type: 'SIGN_IN', token: jwt })
            },
            signOut: async () => {
                await AsyncStorage.removeItem('userToken')
                dispatch({ type: 'SIGN_OUT' })
            },
            signUp: async (data) => {
                dispatch({ type: 'SIGN_IN', token: 'test' })
            },
            getState: async () => {
                console.log(authState)
                dispatch({ type: 'STATE' })
                console.log(authState)
                console.log(dispatch({ type: 'STATE' }))
            },
        }),
        []
    )

    return (
        <AuthContext.Provider value={authContext}>
            <NavigationContainer>
                <Stack.Navigator>
                    {authState.isLoading ? (
                        <Stack.Screen
                            name="Splash"
                            component={SplashScreen}
                            options={{ headerShown: false }}
                        />
                    ) : authState.userToken == null ? (
                        <Stack.Screen
                            name="SignIn"
                            component={SignInScreen}
                            options={{
                                headerShown: false,
                                animationTypeForReplace: authState.isSignOut
                                    ? 'pop'
                                    : 'push',
                            }}
                        />
                    ) : (
                        //User is Signed in
                        <Stack.Screen
                            name="Drawer"
                            component={DrawerNavigator}
                            options={{ headerShown: false }}
                        />
                    )}
                </Stack.Navigator>
            </NavigationContainer>
        </AuthContext.Provider>
    )
}

const styles = StyleSheet.create({
    container: {
        flex: 1,
        backgroundColor: '#fff',
        alignItems: 'center',
        justifyContent: 'center',
    },
})
