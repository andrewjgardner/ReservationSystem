import {
    DrawerContentScrollView,
    DrawerItemList,
    DrawerItem,
} from '@react-navigation/drawer'

export function CustomDrawerContent(props) {
    return (
        <DrawerContentScrollView {...props}>
            <DrawerItem label="Test" onPress={() => alert('Test')} />
            <DrawerItemList {...props} />
        </DrawerContentScrollView>
    )
}
