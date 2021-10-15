export async function getTeam(id) {
    let url = `http://localhost:3030/data/teams/${id}`
    let method = {
        headers: {
            'Content-Type': 'aplication/json'
        },
        method: 'Get',
    }
    let $fetch = await fetch(url, method);
    let body = await $fetch.json();
    console.log(body);
    return body;
}