import { html } from "./../node_modules/lit-html/lit-html.js";
let spasec=' ';
export let tr = (elemnt) => html`
    <tr>
        <th>${elemnt.firstName+spasec+  elemnt.lastName}</th>
        <th>${elemnt.email}</th>
        <th>${elemnt.course}</th>
    </tr>
`;

export let allExport =(info)=>html`
${info.map(elemnt=>tr(elemnt))}
`;