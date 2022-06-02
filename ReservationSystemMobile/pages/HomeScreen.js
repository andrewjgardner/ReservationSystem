import { View, Text, Button, FlatList } from 'react-native'
import { useContext, useState } from 'react'
import { AuthContext } from '../App'
import { apiFetch, getReservations } from '../services/FetchService'
import { renderReservation } from '../components/Reservation'

export function HomeScreen({ navigation }) {
    const { signOut } = useContext(AuthContext)
    const [result, setResult] = useState('')
    const [reservations, setReservations] = useState([])

    async function handleClickReservations() {
        const data = await getReservations()
        setReservations(data)
    }

    return (
        <View>
            <Text>Home Screen</Text>
            <Button title="Sign Out" onPress={signOut} />
            <Button
                title="Get Reservations"
                onPress={() => handleClickReservations()}
            />
            <Text>{result}</Text>
            <FlatList
                data={reservations}
                renderItem={({ item }) => renderReservation(item)}
                keyExtractor={(item) => item.id}
            />
        </View>
    )
}
