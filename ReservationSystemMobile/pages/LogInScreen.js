import { View, Text, Button } from "react-native";

export function LogInScreen({navigation}) {
    return (
        <View style={{ flex: 1, alignItems: 'center', justifyContent: 'center' }}>
            <Button
                title="Go to Home Screen"
                     onPress={() => navigation.navigate('Home')}


            />
            <Text>Log In Screen</Text>
        </View>
    );
  }