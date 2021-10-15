import { createFormObject } from '../helpers/FormatObject/createFormObject.js';
import { postCreate } from '../helpers/Post/post-create.js';
import { html } from '../node_modules/lit-html/lit-html.js';

let create = async e => {
    e.preventDefault();

    document.getElementById('err').style.display = 'none';
    let formObject = createFormObject(e.target)
    let name = formObject.name;
    let logoUrl = formObject.logoUrl;
    let description = formObject.description;
    let wrong = false;
    if (name.length < 4) {
        wrong = true;
    }
    if (logoUrl === '') {
        wrong = true;
    }
    if (description.length < 10) {
        wrong = true;
    }

    try {
        if (wrong) {
            throw Error();
        }
        let body = {
            name,
            logoUrl,
            description
        };
        postCreate(body);
    } catch (error) {
        document.getElementById('err').style.display = 'block';
    }

}


export let createTemplate = () => html`
<section id="create">
    <article class="narrow">
        <header class="pad-med">
            <h1>New Team</h1>
        </header>
        <form @submit="${create}" id="create-form" class="main-form pad-large">
            <div id="err" style="display: none" class="error">Error message.</div>
            <label>Team name: <input type="text" name="name"></label>
            <label>Logo URL: <input type="text" name="logoUrl"></label>
            <label>Description: <textarea name="description"></textarea></label>
            <input class="action cta" type="submit" value="Create Team">
        </form>
    </article>
</section>
`