export async function getFurniture(id) {
    let url = `http://localhost:3030/data/catalog/${id}`
    let method = {
        headers: {
            'Content-Type': 'aplication/json'
        },
        method: 'Get',
    }
    let $fetch = await fetch(url, method);
    let body = await $fetch.json();
    return body;
}