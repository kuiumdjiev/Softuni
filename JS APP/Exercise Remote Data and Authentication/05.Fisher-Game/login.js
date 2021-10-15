let registrationForm = document.getElementById('register-form');
registrationForm.addEventListener('submit', registar);
let loginForm = document.getElementById('login-form');
loginForm.addEventListener('submit',login)

async function login(e) {
    e.preventDefault();
    let form = e.target;
    let fromDate = new FormData(form);

    let email = fromDate.get('email');
    let password = fromDate.get('password');
   
    let user = {
        email: email,
        password: password
    };

    let url = 'http:localhost:3030/users/login';

    let method = {
        headers: {
            'Content-Type': 'aplication/json'
        },
        method: 'Post',
        body: JSON.stringify(user)
    }

    try {
        let response = await fetch(url, method);
        let result = await response.json()

        sessionStorage.setItem('id', result._id);
        sessionStorage.setItem('token', result.accessToken);

    } catch (error) {
        alert('Wrong password or email')
    }

   
}
async function registar(e) {
    e.preventDefault();
    let form = e.target;
    let fromDate = new FormData(form);

    let email = fromDate.get('email');
    let password = fromDate.get('password');
    let rePassword = fromDate.get('rePass');
    if (password != rePassword) {
        alert('Error');
        return;
    }
    let user = {
        email: email,
        password: password
    };

    let url = 'http:localhost:3030/users/register';

    let method = {
        headers: {
            'Content-Type': 'aplication/json'
        },
        method: 'Post',
        body: JSON.stringify(user)
    }

    let response = await  fetch(url, method);
    let result = await response.json()
    console.log(result);
    
    localStorage.setItem('token', result.accessToken)
}