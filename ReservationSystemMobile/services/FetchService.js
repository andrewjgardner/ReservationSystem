const endpoint = 'https://localhost:7156/api/'

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
