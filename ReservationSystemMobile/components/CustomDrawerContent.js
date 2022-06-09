import {
    DrawerContentScrollView,
    DrawerItemList,
    DrawerItem,
} from '@react-navigation/drawer'
import { View, StyleSheet } from 'react-native'
import { UserDetails } from './UserDetails'
import { useContext } from 'react'
import { AuthContext } from '../App'

export function CustomDrawerContent(props) {
    const { signOut } = useContext(AuthContext)
    return (
        <DrawerContentScrollView
            contentContainerStyle={{ justifyContent: 'space-between', flex: 1 }}
            {...props}
        >
            <View>
                <UserDetails />
                <DrawerItemList {...props} />
            </View>
            <View style={styles.signOutDrawerItem}>
                <DrawerItem label="Sign Out" onPress={() => signOut()} />
            </View>
        </DrawerContentScrollView>
    )
}

const styles = StyleSheet.create({
    signOutDrawerItem: {
        textAlign: 'center',
        marginVertical: 10,
        marginHorizontal: 10,
        paddingVertical: 10,
        borderRadius: 4,
        backgroundColor: '#e0efff',
    },
})
