import page from '../../node_modules/page/page.mjs'

export async function postCreate(body) {
    let url = 'http://localhost:3030/data/teams'
    let method = {
        headers: {
            'X-Authorization': sessionStorage.getItem('token'),
            'Content-Type': 'aplication/json'
        },
        method: 'Post',
        body: JSON.stringify(body)
    }
    let response = await fetch(url, method);
    let result = await response.json()
    console.log(result);
    page.redirect(`/details/${result._id}`);
}