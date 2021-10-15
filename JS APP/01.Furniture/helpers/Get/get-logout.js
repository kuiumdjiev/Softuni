export async function getLogout() {
    let url = `http://localhost:3030/users/logout`
    let method = {
        headers: {
            'Content-Type': 'aplication/json'
        },
        method: 'Get',
    }
    let $fetch = await fetch(url, method);
    let body = await $fetch.json();
    console.log(body);
    sessionStorage.removeItem('id');
    sessionStorage.removeItem('token');
    sessionStorage.removeItem('еmail');
}