import page from '../../node_modules/page/page.mjs'

export async function deleteFurniture( id) {
    let url = `http://localhost:3030/data/catalog/${id}`



    let method = {
        headers: {
            'X-Authorization':sessionStorage.getItem('token'),
            'Content-Type': 'aplication/json'
        },
        method: 'Delete'
    }

    await fetch(url, method);
    page.redirect('/');
}