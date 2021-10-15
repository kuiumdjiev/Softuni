import page from '../../node_modules/page/page.mjs'

export async function postRegister(body) {
    let url = 'http://localhost:3030/users/register'



    let method = {
        headers: {
            'Content-Type': 'aplication/json'
        },
        method: 'Post',
        body: JSON.stringify(body)
    }

    try {
        let response = await fetch(url, method);
        let result = await response.json()

        page.redirect('/');
        

    } catch (error) {
        alert(error);
        return
    }
}