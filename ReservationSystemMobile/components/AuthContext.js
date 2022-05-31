import { createContext, useReducer, useContext, useEffect, memo } from 'react'
import { authActions, initialState, reducer } from './Reducer'

const authContext = createContext({
    state: {},
    dispatch: () => {},
    actions: {},
})

function AuthContext({ children }) {
    const [authState, dispatch] = useReducer(reducer, initialState)

    useEffect(() => {
        authActions.restoreToken()
    }, [])

    return (
        <authContext.Provider value={{ authState, dispatch, authActions }}>
            {children}
        </authContext.Provider>
    )
}

export const useAuthContext = () => useContext(authContext)
export default AuthContext
