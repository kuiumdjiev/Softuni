export async function delGuy(id) {
    let url = `http://localhost:3030/data/members/${id}`
    let method = {
        headers: {
            'Content-Type': 'aplication/json'
        },
        method: 'Delete'
    }
     await fetch(url, method);
}