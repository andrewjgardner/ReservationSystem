import { View, Text, Button, FlatList } from 'react-native'
import { useContext, useState } from 'react'
import { AuthContext } from '../components'
import { apiFetch } from '../services/FetchService'
import { renderReservation } from '../components/Reservation'

export function HomeScreen({ navigation }) {
    const { signOut, getRoles, getLoggedInUser, getReservations } =
        useContext(AuthContext)

    const [result, setResult] = useState('')
    const [reservations, setReservations] = useState([])

    async function handleClick(f) {
        const data = await f()
        setResult(JSON.stringify(data))
    }

    async function handleClickReservations() {
        const data = await getReservations()
        setReservations(data)
    }

    return (
        <View>
            <Text>Home Screen</Text>
            <Button title="Sign Out" onPress={signOut} />
            <Button title="Get Roles" onPress={() => handleClick(getRoles)} />
            <Button
                title="Get User Details"
                onPress={() => handleClick(getLoggedInUser)}
            />
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
