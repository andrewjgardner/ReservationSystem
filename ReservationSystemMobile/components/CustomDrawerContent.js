import {
    DrawerContentScrollView,
    DrawerItemList,
    DrawerItem,
} from '@react-navigation/drawer'
import { UserDetails } from './UserDetails'

export function CustomDrawerContent(props) {
    return (
        <DrawerContentScrollView {...props}>
            <UserDetails />
            <DrawerItem label="Test" onPress={() => alert('Test')} />
            <DrawerItemList {...props} />
        </DrawerContentScrollView>
    )
}
