import { createFormObject } from '../helpers/FormatObject/createFormObject.js';
import { postLogin } from '../helpers/Post/post-login.js';
import { html } from '../node_modules/lit-html/lit-html.js';

let login = async e => {
    e.preventDefault();

    document.getElementById('err').style.display = 'none';
    let formObject = createFormObject(e.target)
    let email = formObject.email;
    let password = formObject.password;

    try {

        let body ={email,
            password,
        };
        postLogin(body);
    } catch (error) {
        document.getElementById('err').style.display = 'block';
    }

}


export let loginTemplate = () => html`
<section id="login">
    <article class="narrow">
        <header class="pad-med">
            <h1>Login</h1>
        </header>
        <form @submit="${login}" id="login-form" class="main-form pad-large">
            <div id="err"  style="display: none" class="error">Error message.</div>
            <label>E-mail: <input type="text" name="email"></label>
            <label>Password: <input type="password" name="password"></label>
            <input class="action cta" type="submit" value="Sign In">
        </form>
        <footer class="pad-small">Don't have an account? <a href="#" class="invert">Sign up here</a>
        </footer>
    </article>
</section>
`