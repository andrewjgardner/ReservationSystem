import { useState } from 'react'
import { createDrawerNavigator } from '@react-navigation/drawer'
import { SignOutScreen, HomeScreen } from '../pages'
import { CustomDrawerContent } from './CustomDrawerContent'
import { getLoggedInUser } from '../services/FetchService'

const Drawer = createDrawerNavigator()

export function DrawerNavigator() {
    const [user, setUser] = useState(getUser())

    async function getUser() {
        const getUser = await getLoggedInUser()
        setUser(getUser.email)
    }

    return (
        <Drawer.Navigator
            useLegacyImplementation={true}
            drawerContent={(props) => <CustomDrawerContent {...props} />}
        >
            <Drawer.Screen name="Home" component={HomeScreen} />
            <Drawer.Screen name="Sign Out" component={SignOutScreen} />
        </Drawer.Navigator>
    )
}
