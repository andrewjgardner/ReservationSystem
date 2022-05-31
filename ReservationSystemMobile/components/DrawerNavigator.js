import { createDrawerNavigator } from '@react-navigation/drawer'
import { SignOutScreen, HomeScreen } from '../pages'
import { CustomDrawerContent } from './CustomDrawerContent'

const Drawer = createDrawerNavigator()

export function DrawerNavigator() {
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
