import { View, Text } from 'react-native'

export function renderReservation(reservation) {
    return (
        <View key={reservation.id}>
            <Text>Reservation</Text>
            <Text>{reservation.startTime}</Text>
            <Text>{reservation.guests}</Text>
            <Text>{reservation.comments}</Text>
        </View>
    )
}
