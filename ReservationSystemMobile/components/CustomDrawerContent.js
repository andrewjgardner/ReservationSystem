import {
    DrawerContentScrollView,
    DrawerItemList,
    DrawerItem,
} from '@react-navigation/drawer'
import { View } from 'react-native-web'
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
            <DrawerItem
                label="Sign Out"
                onPress={() => signOut()} 
            />
        </DrawerContentScrollView>
    )
}
