import { View, Text, Button } from "react-native";

export function HomeScreen({navigation}) {
    return (
      <View style={{ flex: 1, alignItems: 'center', justifyContent: 'center' }}>
        <Button
          title="Log In"
          onPress={() => navigation.}/>
        <Text>Home Screen</Text>
      </View>
    );
  }