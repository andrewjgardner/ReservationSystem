import { StackNavigator, useAuthContext } from './components'
import AuthContext from './components/AuthContext'
import { Text } from 'react-native'
import { useEffect } from 'react'

export default function App() {
    return (
        <AuthContext>
            <StackNavigator />
        </AuthContext>
        // <AuthContextProvider>
        //     <StackNavigator />
        // </AuthContextProvider>
    )
}
