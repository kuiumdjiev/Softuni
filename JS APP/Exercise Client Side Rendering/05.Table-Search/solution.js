import {allExport} from './template.js';
import { render } from "./../node_modules/lit-html/lit-html.js";

let url = 'http://localhost:3030/jsonstore/advanced/table';
let $fetch= await fetch(url);
let body = Object.values(await $fetch.json());

let tbody = document.getElementsByTagName('tbody')[0];
render(allExport(body),tbody)

let btn =   document.getElementById('searchBtn');
btn.addEventListener('click', add);

function add() {
   let input = document.getElementById('searchField');
   let childrens=tbody.children;
  for (let child in  childrens) {

   let obj={};
   for (let childOfChild in childrens[child].children) {
     obj.childrens[child][childOfChild]=childrens[child][childOfChild].value
   }
   
  }
}

  