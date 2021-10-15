import { render } from '../node_modules/lit-html/lit-html.js';

export function myFurniture(context) {
    render(context.myFurniture,document.getElementById('viewContainer'));
}