import { View, Text, StyleSheet } from 'react-native'
import { useEffect, useState } from 'react'
import { getLoggedInUser, getRoles } from '../services/FetchService'

export function UserDetails() {
    const [user, setUser] = useState('')

    useEffect(() => {
        getLoggedInUser().then((data) => {
            console.log(data)
            setUser(data)
        })
    }, [])

    return (
        <View style={styles.details}>
            <Text style={styles.detailsText}>User Details</Text>
            <Text>{user.email}</Text>
        </View>
    )
}

const styles = StyleSheet.create({
    details: {
        flex: 1,
        justifyContent: 'center',
        alignContent: 'center',
        textAlign: 'center',
        marginHorizontal: 10,
        marginBottom: 20,
        marginTop: 5,
        paddingVertical: 10,
        borderRadius: 4,
        backgroundColor: '#e0efff',
    },
    detailsText: {
        fontSize: 20,
        fontWeight: 'bold',
    },
})
