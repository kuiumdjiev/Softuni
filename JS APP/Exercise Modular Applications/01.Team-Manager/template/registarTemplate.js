import { createFormObject } from '../helpers/FormatObject/createFormObject.js';
import { postRegister } from '../helpers/Post/post-registar.js';
import { html } from '../node_modules/lit-html/lit-html.js';

let registar = async e => {
    e.preventDefault();
  
    document.getElementById('err').style.display='none';

    let wrong = false;

    let formObject = createFormObject(e.target)
    let email = formObject.email;
    let password = formObject.password;
    let rePass = formObject.repass;
    let username = formObject.username;

    if (email == '' || username == '' || password == '' || rePass == '') {
        wrong = true
    }
    if (password !== rePass) {
        wrong = true
    }
    if (username.length < 3) {
        wrong = true
    }
    if (password.length < 3) {
        wrong = true
    }
    try {
        if (wrong) {
            throw Error();
        }
        let body = {

            email,
            password,
    
        };
        postRegister(body);
    } catch (error) {
        document.getElementById('err').style.display = 'block';
    }
  
}


export let registarTemplate = () => html`
<section id="register">
    <article class="narrow">
        <header class="pad-med">
            <h1>Register</h1>
        </header>
        <form @submit="${registar}" id="register-form" class="main-form pad-large">
            <div id="err" style="display: none" class="error">Error message.</div>
            <label>E-mail: <input type="text" name="email"></label>
            <label>Username: <input type="text" name="username"></label>
            <label>Password: <input type="password" name="password"></label>
            <label>Repeat: <input type="password" name="repass"></label>
            <input class="action cta" type="submit" value="Create Account">
        </form>
        <footer class="pad-small">Already have an account? <a href="#" class="invert">Sign in here</a>
        </footer>
    </article>
</section>
`