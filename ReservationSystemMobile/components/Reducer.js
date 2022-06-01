
import { apiFetch } from "../services/FetchService"

export function reducer(prevState, action) {
    switch (action.type) {
        case 'RESTORE_TOKEN':
            return {
                ...prevState,
                userToken: action.token,
                isLoading: false,
            }
        case 'SIGN_IN':
            return {
                ...prevState,
                isSignOut: false,
                userToken: action.token,
            }
        case 'SIGN_OUT':
            return {
                ...prevState,
                isSignOut: true,
                userToken: null,
            }
        case 'STATE':
            return prevState
    }
}

export const initialState = {
    isLoading: true,
    isSignOut: false,
    userToken: null,
}

export const authActions = {
    signIn: (data) => async (dispatch) => {
        const response = await apiFetch('token', 'POST', {
            email: data.email,
            password: data.password,
        })
        if (response.status !== 200) {
            throw new Error("Couldn't sign in")
        }
        const jwt = await response.text()

        await AsyncStorage.setItem('userToken', jwt)
        dispatch({ type: 'SIGN_IN', token: jwt })
    },
    signOut: () => async (dispatch) => {
        await AsyncStorage.removeItem('userToken')
        dispatch({ type: 'SIGN_OUT' })
    },
    signUp: (data) => async (dispatch) => {
        dispatch({ type: 'SIGN_IN', token: 'test' })
    },
    getLoggedInUser: async () => {
        const jwt = await AsyncStorage.getItem('userToken')
        const response = await apiFetch('user/me', 'GET', null, jwt)
        return await response.json()
    },
    getRoles: async () => {
        const jwt = await AsyncStorage.getItem('userToken')
        const response = await apiFetch('user/roles', 'GET', null, jwt)
        return await response.json()
    },
    getReservations: async () => {
        const jwt = await AsyncStorage.getItem('userToken')
        const response = await apiFetch('user/reservations', 'GET', null, jwt)
        return await response.json()
    },
    restoreToken: () => async (dispatch) => {
        let userToken
        try {
            userToken = await AsyncStorage.getItem('userToken')
        } catch (e) {}
        console.log(userToken)
        dispatch({ type: 'RESTORE_TOKEN', token: userToken })
        return userToken
    },
}
