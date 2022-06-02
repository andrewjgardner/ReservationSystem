import { View, Text } from 'react-native'
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
        <View>
            <Text>User Details</Text>
            <Text>{user.email}</Text>
        </View>
    )
}
