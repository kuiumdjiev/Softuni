export async function getFurnitures() {
    let url = `http://localhost:3030/data/catalog`
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