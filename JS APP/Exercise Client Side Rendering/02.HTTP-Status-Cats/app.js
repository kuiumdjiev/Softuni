import {cats} from "./catSeeder.js";
import { render } from "./../node_modules/lit-html/lit-html.js";
import {catsTemplate} from "./template.js"

let selection = document.getElementById('allCats');
render(catsTemplate(cats), selection);