import { render } from '../node_modules/lit-html/lit-html.js';

export function updateFurniture(context) {
    render(context.updateFurniture,document.getElementById('viewContainer'));
}