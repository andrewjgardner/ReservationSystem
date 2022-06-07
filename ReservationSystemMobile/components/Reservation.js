import { View, Text, StyleSheet } from 'react-native'

export function renderReservation(reservation) {
    const date = new Date(reservation.startTime)
    const dateOptions = {
        weekday: 'long',
        year: 'numeric',
        month: 'long',
        day: 'numeric',
    }
    return (
        <View key={reservation.id} style={styles.item}>
            <Text style={styles.dateFont}>
                {date.toLocaleDateString('en-AU', dateOptions)}
            </Text>
            <Text>{date.toLocaleTimeString()}</Text>
            <Text>Reservation under name: {reservation.name}</Text>
            <Text>No of Guests: {reservation.guests}</Text>
            {reservation.comments != '' && (
                <Text>Comments: {reservation.comments}</Text>
            )}
        </View>
    )
}


const styles = StyleSheet.create({
    item: {
        shadowColor: '#000',
        shadowRadius: 2,
        backgroundColor: '#e0efff',
        padding: 20,
        marginVertical: 8,
        marginHorizontal: 16,
    },
    title: {
        fontSize: 32,
    },
    dateFont: {
        fontSize: 24,
    },
})
