import { render } from "./../node_modules/lit-html/lit-html.js";
import {$ul} from "./template.js";

let btn = document.getElementById('btnLoadTowns');
btn.addEventListener('click',add);

let form = document.getElementsByClassName('content')[0];

let root = document.getElementById('root');

function add(e) {
   
        e.preventDefault();
        let data = new FormData(form);
        let towns = data.get('towns');
        let splitTowns = towns.split(', ');
        render($ul(splitTowns),root);   
}