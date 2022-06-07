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
        console.log(splitArrayOnDays(data))
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

function splitArrayOnDays(array) {
    debugger
    let days = []
    let day = []
    array.forEach((p) => {
        if (day.length === 0) {
            day.push(p)
        } else {
            if (
                new Date(day[0].date).getDate() === new Date(p.date).getDate()
            ) {
                day.push(p)
            } else {
                days.push(day)
                day = []
                day.push(p)
            }
        }
    })
    days.push(day)
    return days
}
