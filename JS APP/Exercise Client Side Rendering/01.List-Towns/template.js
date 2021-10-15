import { html } from "./../node_modules/lit-html/lit-html.js";

export let $li =(element) =>html`<li>${element}</li>`;

export let $ul =(arr)=>html`<ul>${arr.map(element =>$li(element))}</ul>`;