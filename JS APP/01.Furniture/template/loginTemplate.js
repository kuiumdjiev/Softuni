import { createFormObject } from "../helpers/formatObject/formatObject.js";
import { postLogin } from "../helpers/Post/post-login.js";
import { html } from "../node_modules/lit-html/lit-html.js"


let login = async e => {
    e.preventDefault();
    let formObject = createFormObject(e.target)
    let email = formObject.email;
    let password = formObject.password;

    if (email == '' || password == '' ) {
        alert('Please fill all fields.');
        return;
    }
    
    let body = {
        email,
        password,
    };
    postLogin(body);
}

export let loginTemplate=()=>html`
 <div class="row space-top">
            <div class="col-md-12">
                <h1>Login User</h1>
                <p>Please fill all fields.</p>
            </div>
        </div>
        <form @submit="${login}">
            <div class="row space-top">
                <div class="col-md-4">
                    <div class="form-group">
                        <label class="form-control-label" for="email">Email</label>
                        <input class="form-control" id="email" type="text" name="email">
                    </div>
                    <div class="form-group">
                        <label class="form-control-label" for="password">Password</label>
                        <input class="form-control" id="password" type="password" name="password">
                    </div>
                    <input type="submit" class="btn btn-primary" value="Login" />
                </div>
            </div>
        </form>
`