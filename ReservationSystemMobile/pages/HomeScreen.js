import { View, Text, Button, FlatList } from 'react-native'
import { useContext, useEffect, useState } from 'react'
import { AuthContext } from '../App'
import { apiFetch, getReservations } from '../services/FetchService'
import { renderReservation } from '../components/Reservation'

export function HomeScreen({ navigation }) {
    const [result, setResult] = useState('')
    const [reservations, setReservations] = useState([])

    async function handleGetReservations() {
        const data = await getReservations()
        if (data.length == 0) {
            setResult('No reservations found')
            return
        }
        setResult('')
        setReservations(data)
    }

    useEffect(() => {
        handleGetReservations()
    }, [])

    return (
        <View>
            <Text>{result}</Text>
            <FlatList
                data={reservations}
                renderItem={({ item }) => renderReservation(item)}
                keyExtractor={(item) => item.id}
            />
        </View>
    )
}
