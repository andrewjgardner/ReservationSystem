import AsyncStorage from '@react-native-async-storage/async-storage'
const endpoint = 'https://tcs122.azurewebsites.net/api/'

//Arthur's fetch function
export async function apiFetch(url, method, body, jwt) {
    return fetch(`${endpoint}${url}`, {
        method: method,
        headers: {
            'Content-Type': 'application/json',
            Authorization: jwt ? `Bearer ${jwt}` : '',
        },
        body: body ? JSON.stringify(body) : null,
    }).catch((error) => {
        // Return a response anyway so that it can be handled elsewhere
        return new Response(null, {
            status: 500,
            statusText: error.message,
        })
    })
}

export async function getLoggedInUser() {
    const jwt = await AsyncStorage.getItem('userToken')
    const response = await apiFetch('user/me', 'GET', null, jwt)
    return await response.json()
}
export async function getRoles() {
    const jwt = await AsyncStorage.getItem('userToken')
    const response = await apiFetch('user/roles', 'GET', null, jwt)
    return await response.json()
}
export async function getReservations() {
    const jwt = await AsyncStorage.getItem('userToken')
    const response = await apiFetch('user/reservations', 'GET', null, jwt)
    return await response.json()
}
