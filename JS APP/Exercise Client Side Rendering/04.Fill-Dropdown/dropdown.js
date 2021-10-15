import { menu } from './template.js';

let url = 'http://localhost:3030/jsonstore/advanced/dropdown';
let selection = document.getElementById('menu');

let input = document.getElementsByTagName('input')[0];

let btn = document.getElementById('btn');
btn.addEventListener('click', add);

load();
async function load() {
    let $fetch = await fetch(url);
    let body = await $fetch.json();
    let info = Object.values(body);
    btn.disabled = false;
    render(undefined,selection)
    render(menu(info), selection)
}
async function add(e) {
    e.preventDefault();
    let body = JSON.stringify({text:input.value});
    let method = {
        headers: { 'Content-type': 'aplication/json' },
        method: 'Post',
        body
    }
    await fetch(url, method);
    load();
}


