export async function getMyFurnitures(id) {
    let url = `http://localhost:3030/data/catalog?where=_ownerId%3D%22${id}%22`
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