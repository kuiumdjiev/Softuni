export async function getMembers(id) {
    let url = `http://localhost:3030/data/members?where=status%3D%22member%22`
    let method = {
        headers: {
            'Content-Type': 'aplication/json'
        },
        method: 'Get',
    }
    let $fetch = await fetch(url, method);
    let body = await $fetch.json();
    console.log(body);
    return body.filter(x=>x.teamId===id);
}