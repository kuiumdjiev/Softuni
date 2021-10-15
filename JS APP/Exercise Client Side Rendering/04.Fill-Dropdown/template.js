import { html } from "./../node_modules/lit-html/lit-html.js";

export let option =(txt)=>html`<option value=${txt._id}>${txt.text}</option>`

export let menu =(arr)=>html `${arr.map(x=>option(x))}`