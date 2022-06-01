import { createContext, useReducer, useContext, useEffect, memo } from 'react'
import { authActions as pActions, initialState, reducer } from './Reducer'

const authContext = createContext({
    state: {},
    dispatch: () => {},
    actions: {},
})

function AuthContext({ children }) {
    const [authState, dispatch] = useReducer(reducer, initialState)

    useEffect(() => {
        pActions.restoreToken()(dispatch)
    }, [])

    return (
        <authContext.Provider
            value={{ authState, dispatch, actions: pActions }}
        >
            {children}
        </authContext.Provider>
    )
}

export const useAuthContext = () => useContext(authContext)
export default AuthContext
