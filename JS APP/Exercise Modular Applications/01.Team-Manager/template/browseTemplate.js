import { html, nothing } from '../node_modules/lit-html/lit-html.js';

export let browseTemplate = (data) => html`
<a href="/" class="site-logo">Team Manager</a>
<section id="browse">

    <article class="pad-med">
        <h1>Team Browser</h1>
    </article>

    ${sessionStorage.getItem('token') ? loginView() : nothing}
    ${data.map(x => singleTemplate(x))}
</section>
`


export let loginView = () => html`
<article class="layout narrow">
    <div class="pad-small"><a href="/create" class="action cta">Create Team</a></div>
</article>
`
export let singleTemplate = (data) => html`
<article class="layout">
    <img src="${data.logoUrl}" class="team-logo left-col">
    <div class="tm-preview">
        <h2>${data.name}</h2>
        <p>${data.description}</p>
        <span class="details">${data.$count} Members</span>
        <div><a href="/details/${data._id}" class="action">See details</a></div>
    </div>
</article>
`
