import { render } from '../node_modules/lit-html/lit-html.js';

export function home(context) {
    render(context.furnitures,document.getElementById('viewContainer'));
    render(context.nav, document.getElementById('navigation'));
}