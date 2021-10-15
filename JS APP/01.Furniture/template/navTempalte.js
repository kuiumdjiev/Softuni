import { getLogout } from "../helpers/Get/get-logout.js"
import { html } from "../node_modules/lit-html/lit-html.js"
export let navTemplate = () => html`
    <h1><a href="/">Furniture Store</a></h1>

<nav>  
<a id="catalogLink" href="/dashboard" class="">Dashboard</a>
${sessionStorage.getItem('token')!==null?loginView():guestView()}
</nav>
`
export let guestView =()=> html`<div id="guest">
    <a id="loginLink" href="/login">Login</a>
    <a id="registerLink" href="/register">Register</a>
</div>
`

export let loginView = () => html` 
<div id="user">
    <a id="createLink" href="/createFurniture" class="active">Create Furniture</a>
    <a id="profileLink" href="/myFurniture" >My Publications</a>
    <a id="logoutBtn" @click="${getLogout}" href="/">Logout</a>
</div>
`