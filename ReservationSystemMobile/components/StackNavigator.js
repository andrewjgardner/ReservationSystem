import { NavigationContainer } from '@react-navigation/native'
import { createNativeStackNavigator } from '@react-navigation/native-stack'
import { useAuthContext } from './AuthContext'
import { useContext, useEffect } from 'react'
import { SplashScreen, SignInScreen } from '../pages'
import { DrawerNavigator } from './DrawerNavigator'

const Stack = createNativeStackNavigator()

export function StackNavigator() {
    const {
        authState,
        dispatch,
        actions: { restoreToken },
    } = useAuthContext()

    return (
        <NavigationContainer>
            <Stack.Navigator>
                {authState.isLoading ? (
                    <Stack.Group>
                        <Stack.Screen name="Splash" component={SplashScreen} />
                    </Stack.Group>
                ) : authState.userToken == null ? (
                    <Stack.Group>
                        <Stack.Screen
                            name="SignIn"
                            component={SignInScreen}
                            options={{
                                title: 'Sign In',
                            }}
                        />
                    </Stack.Group>
                ) : (
                    // User is signed in
                    <Stack.Group>
                        <Stack.Screen
                            name="Drawer"
                            component={DrawerNavigator}
                            options={{ headerShown: false }}
                        />
                    </Stack.Group>
                )}
            </Stack.Navigator>
        </NavigationContainer>
    )
}
