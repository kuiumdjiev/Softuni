import page from '../../node_modules/page/page.mjs'

export async function approveGuy(id) {
    let url = `http://localhost:3030/data/members/${id}`


    let method = {
        headers: {
            'Content-Type': 'aplication/json'
        },
        method: 'Put',
    }

    try {
        let response = await fetch(url, method);
        let result = await response.json()

        page.redirect(`/details/${id}`);
        

    } catch (error) {
        alert(error);
        return
    }
}