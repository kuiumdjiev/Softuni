import {towns} from "./towns.js";
import {townsTemplate,matchesCount} from "./template.js"
import { render } from "./../node_modules/lit-html/lit-html.js";

let divTown = document.getElementById('towns');
let data = towns.map(town => ({name: town}));
render(townsTemplate(data),divTown);

let resultMatches= document.getElementById('result');



let btn = document.getElementById('button');

btn.addEventListener('click', fun)

function fun(e) {
   e.preventDefault();
   
   let searchInput = document.getElementById('searchText');
   let searchText = searchInput.value.toLowerCase();

   let matches  = data.filter(town =>town.name.toLocaleLowerCase().includes(searchText));
   matches.forEach(town =>town.class='active');
   let count = matches.length;
   render(townsTemplate( data),divTown);
   render(matchesCount(count),resultMatches)
}