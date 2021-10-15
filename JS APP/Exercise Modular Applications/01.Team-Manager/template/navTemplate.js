import { getLogout } from '../helpers/Get/get-Logout.js';
import { html } from '../node_modules/lit-html/lit-html.js';





export let navTemplate = () => html`
<a href="/" class="site-logo">Team Manager</a>
<nav>
    ${sessionStorage.getItem('token') ? loginView() : guestView()}
</nav>
`
export let guestView = () => html`
<a href="/browse" class="action">Browse Teams</a>
<a href="/login" class="action">Login</a>
<a href="/registar" class="action">Register</a>
`

export let loginView = () => html`
<a href="#" class="action">My Teams</a>
<a @click="${getLogout}" href="/" class="action">Logout</a>
`