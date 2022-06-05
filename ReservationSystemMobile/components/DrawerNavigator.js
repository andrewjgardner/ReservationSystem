import { useState, useEffect } from 'react'
import { createDrawerNavigator } from '@react-navigation/drawer'
import { SignOutScreen, HomeScreen } from '../pages'
import { CustomDrawerContent } from './CustomDrawerContent'

const Drawer = createDrawerNavigator()

export function DrawerNavigator() {
    const [user, setUser] = useState()

    return (
        <Drawer.Navigator
            useLegacyImplementation={true}
            drawerContent={(props, user) => (
                <CustomDrawerContent {...props} user={user} />
            )}
        >
            <Drawer.Screen name="Reservations" component={HomeScreen} />
        </Drawer.Navigator>
    )
}
