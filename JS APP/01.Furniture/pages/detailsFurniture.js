import { render } from '../node_modules/lit-html/lit-html.js';

export function detailsFurniture(context) {
    render(context.detailsFurniture,document.getElementById('viewContainer'));
}