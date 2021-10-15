import { render } from '../node_modules/lit-html/lit-html.js';

export function createFurniture(context) {
    render(context.createFurniture,document.getElementById('viewContainer'));
}