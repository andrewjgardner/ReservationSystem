import { StatusBar } from 'expo-status-bar'
import { StyleSheet, Text, View } from 'react-native'
import { NavigationContainer, StackActions } from '@react-navigation/native'
import { createNativeStackNavigator } from '@react-navigation/native-stack'
import { HomeScreen, LogInScreen } from './pages'
import { useState, useContext } from 'react'

export default function App() {
    const [isSignedIn, setIsSignedIn] = useState(true)
    // const [state, dispatch] = React.useReducer((prevState, action) => {
    //     switch (action.type) {
    //         case 'RESTORE_TOKEN':
    //             return {
    //                 ..prevState,
    //                 userToken: action.token,
    //                 isLoading: false,
    //             };

    //         case 'SIGN_IN':
    //             return {

    //             };
    //         default:
    //             break;
    //     }
    // })
    return (
        <NavigationContainer>
            <Stack.Navigator>
                <Stack.Screen name="LogIn" component={LogInScreen} />
                <Stack.Screen name="Home" component={HomeScreen} />
            </Stack.Navigator>
        </NavigationContainer>
    )
}

const Stack = createNativeStackNavigator()

const styles = StyleSheet.create({
    container: {
        flex: 1,
        backgroundColor: '#fff',
        alignItems: 'center',
        justifyContent: 'center',
    },
})
