import page from '../../node_modules/page/page.mjs'

export async function postLogin(body) {
    let url = 'http://localhost:3030/users/login'



    let method = {
        headers: {
            'Content-Type': 'aplication/json'
        },
        method: 'Post',
        body: JSON.stringify(body)
    }

    try {
        let response = await fetch(url, method);

        if (response.statusText !== 'OK') {
            throw Error();
        }
        let result = await response.json()
        console.log(result);

        sessionStorage.setItem('id', result._id);
        sessionStorage.setItem('token', result.accessToken);
        sessionStorage.setItem('еmail', result.email);
        page.redirect('/');


    } catch (error) {
        alert('Wrong password or email');
        return
    }
}