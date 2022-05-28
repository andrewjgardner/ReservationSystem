import { View, Text, Button } from 'react-native'
import { useContext, useState } from 'react'
import { AuthContext } from '../App'
import { apiFetch } from '../services/FetchService'
import AsyncStorage from '@react-native-async-storage/async-storage'

export function HomeScreen({ navigation }) {
    const { signOut, getRoles, getLoggedInUser, getReservations } = useContext(AuthContext)
    const [result, setResult] = useState('')

    async function handleGetRoles() {
        const data = await getRoles()
        setResult(JSON.stringify(data))
    }

    async function handleGetLoggedInUser() {
        const data = await getLoggedInUser()
        setResult(JSON.stringify(data))
    }
    
    async function handleGetReservations(){
        const data = await getReservations()
        setResult(JSON.stringify(data))        
    }

    return (
        <View>
            <Text>Home Screen</Text>
            <Button title="Sign Out" onPress={signOut} />
            <Button title="Get Roles" onPress={handleGetRoles} />
            <Button title="Get User Details" onPress={handleGetLoggedInUser} />
            <Button title="Get Reservations" onPress={handleGetReservations} />
            <Text>{result}</Text>
        </View>
    )
}
