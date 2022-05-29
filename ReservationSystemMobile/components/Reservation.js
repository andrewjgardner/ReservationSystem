import { View, Text, StyleSheet } from 'react-native'

export function renderReservation(reservation) {
    return (
        <View key={reservation.id} style={styles.item}>
            <Text style={styles.title}>Reservation</Text>
            <Text>{reservation.startTime}</Text>
            <Text>{reservation.guests}</Text>
            <Text>{reservation.comments}</Text>
        </View>
    )
}

const styles = StyleSheet.create({
    item: {
        backgroundColor: '#f9c2ff',
        padding: 20,
        marginVertical: 8,
        marginHorizontal: 16,
    },
    title: {
        fontSize: 32,
    },
})
